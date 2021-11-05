using Project.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    private UserNameChangeView _view;
    private UserNameChangeUseCase _useCase;
    private void Start()
    {
        _useCase = new UserNameChangeUseCase(_view);
    }
}

public class UserNameChangeView : MonoBehaviour, IUserNamePresentation
{
    [SerializeField] private Text _resultText;

    [SerializeField] private Button _button;

    [SerializeField] private InputField _inputField;

    [SerializeField] private Text _nameLabel;


    // Start is called before the first frame update
    void Start()
    {
        _nameLabel.text = PlayerPrefs.GetString("user_name");
        _button.onClick.AddListener((() =>
        {
            UserNameChangeUseCase useCase = new UserNameChangeUseCase(this);
            useCase.ChangeName(_inputField.text);
        }));
    }

    public void ShowChangeNameSuccess(string newName)
    {
        _resultText.text = $"{newName}に変更しました！";
        _nameLabel.text = newName;
    }

    public void ShowChangeNameFailure(string failureName)
    {
        _resultText.text = $"{failureName}には変更できません！";
    }
}
