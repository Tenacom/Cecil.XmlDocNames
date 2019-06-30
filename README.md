![License](https://img.shields.io/github/license/rdeago/cecil-xmldocnames.svg)
![NuGet downloads](https://img.shields.io/nuget/dt/Cecil.XmlDocNames.svg)

![GitHub downloads](https://img.shields.io/github/downloads/rdeago/cecil-xmldocnames/total.svg)
![Release date](https://img.shields.io/github/release-date/rdeago/cecil-xmldocnames.svg)
![Last commit](https://img.shields.io/github/last-commit/rdeago/cecil-xmldocnames.svg)
![Open issues](https://img.shields.io/github/issues-raw/rdeago/cecil-xmldocnames.svg)
![Closed issues](https://img.shields.io/github/issues-closed-raw/rdeago/cecil-xmldocnames.svg)

Cecil.XmlDocNames is a small .NET Standard 2.0 library that generates XmlDoc-style names for Mono.Cecil member references. It has no dependencies apart from the .NET Standard library and Mono.Cecil.

XmlDoc names are also used by [ReSharper](https://www.jetbrains.com/resharper/) in its [external annotations](https://www.jetbrains.com/help/resharper/Code_Analysis__External_Annotations.html) files, which is why I wrote this library in the first place.

If you find this library useful, please **:star: star it**. Thank you!

## Usage

1. Reference the [NuGet package](https://www.nuget.org/packages/Cecil.XmlDocNames).
2. `using Cecil.XmlDocNames;`
3. `string name = myMemberReference.GetXmlDocName();`
where `myMemberReference` can be an instance of one of the following classes:
    * `Mono.Cecil.TypeReference`;
    * `Mono.Cecil.MethodReference`;
    * `Mono.Cecil.PropertyReference`;
    * `Mono.Cecil.FieldReference`;
    * `Mono.Cecil.EventReference`.

---

*Disclaimer:* The author of this library is in no way affiliated to [JetBrains s.r.o.](https://www.jetbrains.com/) (the makers of ReSharper) other than being a satisfied cutomer.