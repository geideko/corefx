//// Licensed to the .NET Foundation under one or more agreements.
//// The .NET Foundation licenses this file to you under the MIT license.
//// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;

using Internal.Cryptography;

namespace System.Security.Cryptography
{
    public sealed partial class Gost3410Cng : Gost3410
    {
        /// <summary>
        ///     Creates a new Gost3410Cng object that will use the specified key. The key's
        ///     <see cref="CngKey.AlgorithmGroup" /> must be Rsa. This constructor
        ///     creates a copy of the key. Hence, the caller can safely dispose of the 
        ///     passed in key and continue using the Gost3410Cng object. 
        /// </summary>
        /// <param name="key">Key to use for Gost3410 operations</param>
        /// <exception cref="ArgumentException">if <paramref name="key" /> is not an Gost3410 key</exception>
        /// <exception cref="ArgumentNullException">if <paramref name="key" /> is null.</exception>
        public Gost3410Cng(CngKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (key.AlgorithmGroup != CngAlgorithmGroup.Gost3410)
                throw new ArgumentException(SR.Cryptography_ArgGost3410RequiresGost3410Key, nameof(key));

            Key = CngAlgorithmCore.Duplicate(key);
        }

        private void ForceSetKeySize(int newKeySize)
        {
            KeySizeValue = newKeySize;
        }

        protected override void Dispose(bool disposing)
        {
            _core.Dispose();
        }

        //public override byte[] SignHash(byte[] hash, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        //{
        //    throw null;
        //}
        //protected override byte[] HashData(System.IO.Stream data, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        //{
        //    throw null;
        //}
        //public override bool VerifyHash(byte[] hash, byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        //{
        //    throw null;
        //}
        //protected override byte[] HashData(byte[] data, int offset, int count, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        //{
        //    throw null;
        //}


        private CngAlgorithmCore _core;
    }
}
