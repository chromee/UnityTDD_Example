using UnityEngine;

namespace Project.Scripts
{
    public class UserNameChangeUseCase
    {
        private readonly IUserNamePresentation _presentation;

        public UserNameChangeUseCase(IUserNamePresentation presentation)
        {
            _presentation = presentation;
        }
        
        public void ChangeName(string newName)
        {
            if (UserNameValidator.Validate(newName))
            {
                PlayerPrefs.SetString("user_name", newName);
                _presentation.ShowChangeNameSuccess(newName);
            }
            else
            {
                _presentation.ShowChangeNameFailure(newName);
            }
        }
    }

    public interface IUserNamePresentation
    {
        void ShowChangeNameSuccess(string newName);
        void ShowChangeNameFailure(string failureName);
    }
}