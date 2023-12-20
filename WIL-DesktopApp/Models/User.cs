using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIL_DesktopApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        private readonly MaterialRepository _materialRepository;
        private readonly RequestRepository _requestRepository;
        private readonly UserRepository? _userRepository;
        //TODO: RequestRepository
        //TODO: UserRepository


        /// <summary>
        /// Provides relevant info about a user.
        /// </summary>
        /// <param name="username">Username of user (used to log in)</param>
        /// <param name="firstName">First name of user</param>
        /// <param name="lastName">Last name of user</param>
        /// <param name="email">Email of user</param>
        /// <param name="type">0 = Employee , 1 = Admin</param>
        public User(string username, string firstName, string lastName, string email, int type, MaterialRepository materialRepository, RequestRepository requestRepository, UserRepository? userRepository = null)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Type = type;
            _materialRepository = materialRepository;
            _requestRepository = requestRepository;
            _userRepository = userRepository;
        }
        public void AddUser(KryptonUser user)
        {
            _userRepository.AddUser(user);
        }
        public void RemoveUser(KryptonUser user)
        {
            _userRepository.RemoveUser(user);
        }
        public void UpdateUser(KryptonUser oldUser, KryptonUser newUser)
        {
            _userRepository.UpdateUser(oldUser, newUser); //Didn't add a null check, be warned
        }

        /// <summary>
        /// Deletes a request from the database given the model
        /// </summary>
        /// <param name="req"></param>
        public void RemoveRequest(Request req)
        {
            _requestRepository.RemoveRequest(req);
        }
        /// <summary>
        /// Returns lite versions of requests, not including attributes or their values
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Request> GetRequests()
        {
            return _requestRepository.GetRequests();
        }
        /// <summary>
        /// Returns a fully populated request given a lite request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Request GetFullRequest(Request req)
        {
            return _requestRepository.GetFullRequest(req);
        }
        /// <summary>
        /// Asynchronously get all materials
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _materialRepository.GetAllMaterialsAsync();
        }

        /// <summary>
        /// Get all materials
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Material> GetAllMaterials()
        {
            return _materialRepository.GetAllMaterials();
        }

        /// <summary>
        /// Asynchronously remove a material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task RemoveMaterialAsync(Material material)
        {
            await _materialRepository.RemoveMaterialAsync(material);
        }

        /// <summary>
        /// Remove a material
        /// </summary>
        /// <param name="material"></param>
        public void RemoveMaterial(Material material)
        {
            _materialRepository.RemoveMaterial(material);
        }
        /// <summary>
        /// Asynchronously add a material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task AddMaterialAsync(Material material)
        {
            await _materialRepository.AddMaterialAsync(material);
        }

        /// <summary>
        /// Add a material
        /// </summary>
        /// <param name="material"></param>
        public void AddMaterial(Material material)
        {
            _materialRepository.AddMaterial(material);
        }

        /// <summary>
        /// Asynchronously update a material
        /// </summary>
        /// <param name="old"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        public async Task UpdateMaterialAsync(Material old, Material updated)
        {
            await _materialRepository.UpdateMaterialAsync(old, updated);
        }

        /// <summary>
        /// Update a material
        /// </summary>
        /// <param name="oldMaterial"></param>
        /// <param name="updatedMaterial"></param>
        public void UpdateMaterial(Material oldMaterial, Material updatedMaterial)
        {
            _materialRepository.UpdateMaterial(oldMaterial, updatedMaterial);
        }

        public List<KryptonUser> GetKryptonUsers() 
        {
            return (List<KryptonUser>)(_userRepository == null ? Enumerable.Empty<KryptonUser>() : _userRepository.GetAllUsers());
        }
    }
}