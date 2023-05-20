using System;
using System.Linq.Expressions;

namespace BaseAPI.Core.Interfaces.Service
{
	public interface IPasswordService
	{
        string EncryptPassword(string unEncryptedPass,out string vectorBase64);
        string EncryptPassword(string unEncryptedPass,string vectorBase64);
        string DecryptPassword(string encryptedPass, string vectorBase64);
        bool CompareEncrytedAndUnencryptedPassword(string encryptedPass, string vectorBase64, string unEncryptedPass);
    }
}

