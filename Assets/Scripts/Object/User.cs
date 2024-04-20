
public class User
{
    public string _id;
    public string _email;
    public string _password;
    public bool _status;
    public string _idCharacter;

    public User()
    {

    }

    public User(string _id,string _email,string _password,bool _status,string _idCharacter)
    {
        this._id = _id;
        this._email = _email;
        this._password = _password;
        this._status = _status;
        this._idCharacter = _idCharacter;
    }
}
