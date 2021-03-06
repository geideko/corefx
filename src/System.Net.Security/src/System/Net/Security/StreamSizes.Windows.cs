// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Net
{
    internal partial struct StreamSizes
    {
        public StreamSizes(SecPkgContext_StreamSizes interopStreamSizes)
        {
            Header = (int)interopStreamSizes.cbHeader;
            Trailer = (int)interopStreamSizes.cbTrailer;
            MaximumMessage = (int)interopStreamSizes.cbMaximumMessage;
        }
    }
}
