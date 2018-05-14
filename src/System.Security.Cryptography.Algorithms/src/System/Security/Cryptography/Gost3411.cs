﻿// Copyright (C) 2006-2012 Крипто-Про. Все права защищены.
//
// Этот файл содержит информацию, являющуюся
// собственностью компании Крипто-Про.
// 
// Любая часть этого файла не может быть скопирована,
// исправлена, переведена на другие языки,
// локализована или модифицирована любым способом,
// откомпилирована, передана по сети с или на
// любую компьютерную систему без предварительного
// заключения соглашения с компанией Крипто-Про.
// 

using Internal.Cryptography;

namespace System.Security.Cryptography
{
    /// <summary>
    /// Абстрактный базовый класс для всех реализаций алгоритма ГОСТ Р 34.11.
    /// Все реализации алгоритма ГОСТ Р 34.11 должны быть отнаследованы от данного класса.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>Создание наследников данного класса позволяет создать конкретную 
    /// реализацию алгоритма ГОСТ Р 34.11.</para>
    /// <para>Основное применение данного класса, это идентификация алгоритма 
    /// ГОСТ Р 34.11 в иерархии криптографических алгоритмов.</para>
    /// </remarks>
    /// 
    /// <doc-sample path="Simple\DocBlock" name="HashBuffer" region="HashBuffer">
    /// Пример, вычисляющий хэш по алгоритму ГОСТ Р 34.11 при помощи класса
    /// <see cref="Gost3411CryptoServiceProvider"/>, унаследованного от 
    /// Gost3411. В примере предполагается, что ранее определена константа 
    /// DATA_SIZE.
    /// </doc-sample>
    /// 
    /// <basedon cref="System.Security.Cryptography.SHA1"/>
    public abstract class Gost3411 : HashAlgorithm
    {
        internal const int GOST3411_SIZE = 256;

        public static new Gost3411 Create() => new Implementation();

        public static new Gost3411 Create(string hashName) => (Gost3411)CryptoConfig.CreateFromName(hashName);

        private sealed class Implementation : Gost3411
        {
            private readonly HashProvider _hashProvider;

            public Implementation()
            {
                _hashProvider = HashProviderDispenser.CreateHashProvider(HashAlgorithmNames.GOST3411);
                HashSizeValue = GOST3411_SIZE;
            }

            protected sealed override void HashCore(byte[] array, int ibStart, int cbSize) =>
                _hashProvider.AppendHashData(array, ibStart, cbSize);

            protected sealed override void HashCore(ReadOnlySpan<byte> source) =>
                _hashProvider.AppendHashData(source);

            protected sealed override byte[] HashFinal() =>
                _hashProvider.FinalizeHashAndReset();

            protected sealed override bool TryHashFinal(Span<byte> destination, out int bytesWritten) =>
                _hashProvider.TryFinalizeHashAndReset(destination, out bytesWritten);

            public sealed override void Initialize()
            {
                // Nothing to do here. We expect HashAlgorithm to invoke HashFinal() and Initialize() as a pair. This reflects the 
                // reality that our native crypto providers (e.g. CNG) expose hash finalization and object reinitialization as an atomic operation.
            }

            protected sealed override void Dispose(bool disposing)
            {
                _hashProvider.Dispose(disposing);
                base.Dispose(disposing);
            }
        }
    }
}
