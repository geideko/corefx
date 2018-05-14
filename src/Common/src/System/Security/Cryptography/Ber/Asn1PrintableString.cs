﻿namespace System.Security.Cryptography
{
	[Serializable]
	class Asn1PrintableString : Asn18BitCharString
	{
		public static readonly Asn1Tag Tag = new Asn1Tag(0, 0, PrintableStringTypeCode);

		public Asn1PrintableString()
			: base(PrintableStringTypeCode)
		{
		}

		public Asn1PrintableString(string data)
			: base(data, PrintableStringTypeCode)
		{
		}

		public override void Decode(Asn1BerDecodeBuffer buffer, bool explicitTagging, int implicitLength)
		{
			Decode(buffer, explicitTagging, implicitLength, Tag);
		}

		public override int Encode(Asn1BerEncodeBuffer buffer, bool explicitTagging)
		{
			return Encode(buffer, explicitTagging, Tag);
		}

		public override void Encode(Asn1BerOutputStream outs, bool explicitTagging)
		{
			outs.EncodeCharString(Value, explicitTagging, Tag);
		}
	}
}
