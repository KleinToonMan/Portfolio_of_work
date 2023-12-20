using KryptonUserDTO = WIL_DesktopApp.DataModels.KryptonUser;
using KryptonUserInfoDTO = WIL_DesktopApp.DataModels.UserInfo;
using KryptonUser = WIL_DesktopApp.Models.KryptonUser;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public class KryptonUserDTOConverter
    {
        /// <summary>
        /// Converts a model to a data transfer object (used for database manipulation)
        /// </summary>
        /// <param name="kusrData"></param>
        /// <returns></returns>

        //The convertion is a bit diffivult cause of the models not matching up
        /*public static User ToUserModel(KryptonUserDTO kusrData)
        {
            return new User(kusrData.Username,"","",1,"",kusrData.UserType,);
        }*/

        /// <summary>
        /// Converts data transfer objects to workable model
        /// </summary>
        /// <param name="usrModel"></param>
        /// <returns></returns>
        public static KryptonUserDTO ToKryptonUserDTO(KryptonUser usrModel)
        {
            return new KryptonUserDTO()
            {
                Username = usrModel.Username,
                UserType = usrModel.Type
            };
        }

        public static KryptonUser ToKryptonUserModel(KryptonUserDTO kryptonUserDTO, KryptonUserInfoDTO kryptonUserInfoDTO)
        {
            return new KryptonUser(kryptonUserDTO.Username, kryptonUserInfoDTO.FirstName, kryptonUserInfoDTO.LastName, kryptonUserInfoDTO.Email, kryptonUserDTO.UserType);
        }
    }
}
