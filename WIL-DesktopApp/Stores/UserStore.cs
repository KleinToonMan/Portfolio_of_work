using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WIL_DesktopApp.Models;
using KryptonUser = WIL_DesktopApp.Models.KryptonUser;

namespace WIL_DesktopApp.Stores
{
    /*
     * <summary>
     * Model of users for login authentication and user permissions.
     * </summary>
     */
    public class UserStore
    {
        private User _user;
        private List<Request> _requests;
        private List<KryptonUser> _users; // ONLY POPULATE LIST IF USER IS AUTHORISED TO EDIT OTHER USERS
        private readonly List<Material> _materials;
        //        private Lazy<Task> _initializeRequestsLazy;
        //        private Lazy<Task>? _initializeUsersLazy;
        private Lazy<Task> _initializeMaterialsLazy;

        public IEnumerable<Request> Requests => _requests;
        public IEnumerable<KryptonUser> Users => _users;
        public IEnumerable<Material> Materials => _materials;

        /// <summary>
        /// Takes in a user model to use for the duration of runtime. Stores a list of requests, materials and if they are an admin, a list of other users.
        /// </summary>
        /// <param name="user"></param>
        public UserStore(User user)
        {
            _user = user;

            _requests = new List<Request>();
            _materials = new List<Material>();
            _users = new List<KryptonUser>();

            _materials.AddRange(user.GetAllMaterials());
            _requests = (List<Request>)user.GetRequests();
            if( user.Type > 0)
            {
                _users = user.GetKryptonUsers().ToList();
            }

            _initializeMaterialsLazy = new Lazy<Task>(InitializeMaterials);
        }
        public void RefreshUsers()
        {
            _users.Clear();
            _users = _user.GetKryptonUsers();
        }

        public void RefreshRequests()
        {
            _requests.Clear();
            _requests = _user.GetRequests().ToList();
        }
        public void AddKryptonUser(KryptonUser user)
        {
            _user.AddUser(user);
            _users.Add(user);
        }
        public List<KryptonUser> GetKryptonUsers()
        {
            return _users;
        }
        public void UpdateKryptonUser(KryptonUser oldUser, KryptonUser newUser)
        {
            _user.UpdateUser(oldUser, newUser);
            //_users[_users.IndexOf(oldUser)] = newUser;
        }
        public void RemoveKryptonUser(KryptonUser user)
        {
            _user.RemoveUser(user);
            RefreshUsers();
        }
        public User GetUser()
        {
            return _user;
        }
        public string GetFirstName()
        {
            return _user.FirstName;
        }

        public string GetLastName()
        {
            return _user.LastName;
        }

        public string GetEmail()
        {
            return _user.Email;
        }

        public int GetUserType()
        {
            return _user.Type;
        }

        /// <summary>
        /// Returns a quote request with fully populated request items (products with attributes)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Request GetFullRequest(Request req)
        {
            return _user.GetFullRequest(req);
        }
        /// <summary>
        /// Deletes a request from the database given a model
        /// </summary>
        /// <param name="req"></param>
        public void RemoveRequest(Request req)
        {
            _user.RemoveRequest(req);
            _requests.Remove(req);
        }
        private async Task InitializeMaterials()
        {
            IEnumerable<Material> materials = await _user.GetAllMaterialsAsync();

            _materials.Clear();
            _materials.AddRange(materials);
        }

        public async Task LoadMaterials()
        {
            try
            {
                await _initializeMaterialsLazy.Value;
            }
            catch (Exception)
            {
                _initializeMaterialsLazy = new Lazy<Task>(InitializeMaterials);
                throw;
            }
        }

        /// <summary>
        /// Returns an array of requests with keywords matching search string
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Request> SearchRequests(string search)
        {
            var query = (from request in _requests
                         where request.Email.Contains(search)
                         || request.Notes.Contains(search)
                         || request.RequestItems.Any(x => x.Name.Contains(search))
                         select request).ToList();
            return query;
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _user.AddMaterialAsync(material);
            _materials.Add(material);
        }

        public void AddMaterials(Material material)
        {
            _user.AddMaterial(material);
            _materials.Add(material);
        }

        public async Task RemoveMaterialAsync(Material material)
        {
            await _user.RemoveMaterialAsync(material);
            _materials.Remove(material);
        }

        public void RemoveMaterial(Material material)
        {
            _user.RemoveMaterial(material);
            _materials.Remove(material);
        }

        public async Task UpdateMaterialAsync(Material oldMaterial, Material newMaterial)
        {
            await _user.UpdateMaterialAsync(oldMaterial, newMaterial);
            int oldIndex = _materials.IndexOf(oldMaterial);
            if (oldIndex != -1)
            {
                _materials[oldIndex] = newMaterial;
            }
            else
            {
                MessageBox.Show("Material not found" + oldMaterial.Name, "Material error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void UpdateMaterial(Material oldMaterial, Material newMaterial)
        {
            _user.UpdateMaterial(oldMaterial, newMaterial);

            int oldIndex = _materials.IndexOf(oldMaterial);

            if (oldIndex != -1)
            {
                _materials[oldIndex] = newMaterial;
            }
            else
            {
                MessageBox.Show("Material not found" + oldMaterial.Name, "Material error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}