![License](https://img.shields.io/github/license/tenacom/Cecil.XmlDocNames.svg)
![NuGet downloads](https://img.shields.io/nuget/dt/Cecil.XmlDocNames.svg)
![GitHub downloads](https://img.shields.io/github/downloads/tenacom/Cecil.XmlDocNames/total.svg)

![Release date](https://img.shields.io/github/release-date/tenacom/Cecil.XmlDocNames.svg)
![Last commit](https://img.shields.io/github/last-commit/tenacom/Cecil.XmlDocNames.svg)
![Open issues](https://img.shields.io/github/issues-raw/tenacom/Cecil.XmlDocNames.svg)
![Closed issues](https://img.shields.io/github/issues-closed-raw/tenacom/Cecil.XmlDocNames.svg)

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