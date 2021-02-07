#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Text;

namespace Teaching.Certification.OA.Data
{
    class PasswordHashService : IPasswordHashService
    {
        private readonly Encoding _encoding;


        public PasswordHashService()
        {
            _encoding = Encoding.UTF8;
        }


        public string ComputeHash(string password)
        {
            var buffer = _encoding.GetBytes(password);

            buffer = AlgorithmUtility.RunSha384(algo => algo.ComputeHash(buffer));

            return Convert.ToBase64String(buffer);
        }

        public bool VerifyHash(string passwordHash, string password)
        {
            var compareHash = ComputeHash(password);

            return compareHash.Equals(passwordHash, StringComparison.Ordinal);
        }

    }
}
