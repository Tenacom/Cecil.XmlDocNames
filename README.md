# ![](https://raw.githubusercontent.com/tenacom/Cecil.XmlDocNames/master/graphics/Cecil.XmlDocNames-128.png) Cecil.XmlDocNames

[![License](https://img.shields.io/github/license/tenacom/Cecil.XmlDocNames.svg)](https://github.com/tenacom/Cecil.XmlDocNames/blob/master/LICENSE)
[![NuGet downloads](https://img.shields.io/nuget/dt/Cecil.XmlDocNames.svg)](https://nuget.org/packages/Cecil.XmlDocNames)
![GitHub downloads](https://img.shields.io/github/downloads/tenacom/Cecil.XmlDocNames/total.svg)
[![Release date](https://img.shields.io/github/release-date/tenacom/Cecil.XmlDocNames.svg)](https://github.com/tenacom/Cecil.XmlDocNames/releases)

[![Last commit](https://img.shields.io/github/last-commit/tenacom/Cecil.XmlDocNames.svg)](https://github.com/tenacom/Cecil.XmlDocNames/commits/master)
[![Open issues](https://img.shields.io/github/issues-raw/tenacom/Cecil.XmlDocNames.svg)](https://github.com/tenacom/Cecil.XmlDocNames/issues?q=is%3Aissue+is%3Aopen+sort%3Aupdated-desc)
[![Closed issues](https://img.shields.io/github/issues-closed-raw/tenacom/Cecil.XmlDocNames.svg)](https://github.com/tenacom/Cecil.XmlDocNames/issues?q=is%3Aissue+is%3Aclosed+sort%3Aupdated-desc)
[![Changelog](https://img.shields.io/badge/changelog-Keep%20a%20Changelog%20v1.0.0-%23E05735)](https://github.com/tenacom/Cecil.XmlDocNames/blob/master/CHANGELOG.md)

`Cecil.XmlDocNames` translates Mono.Cecil member references to XmlDoc-style ID strings.

It's a small, **MIT**-licensed, **.NET Standard 2.0** library. Its only dependency is [`Mono.Cecil`](https://github.com/jbevain/cecil).

## Why this library

With `Cecil.XmlDocNames` you can process assemblies and related XML documentation files at the same time.

Or you can automatically augment XML documentation, based on [code analysis attributes](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis?view=netstandard-2.1) extracted from compiled assemblies.

Or you can generate external annotation files for [ReSharper](https://www.jetbrains.com/resharper/), based on the contents of compiled assemblies... although you don't really have to bother, because that's exactly what our [`ReSharper.ExportAnnotations`](https://github.com/tenacom/ReSharper.ExportAnnotations) library does (using `Cecil.XmlDocNames`, of course.)

If you find this library useful, please **:star: star it**. Thank you!

## Quick start

It's easy as 1 - 2 - 3:

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

The logo for this library is a modified version of [Documentation](https://thenounproject.com/icon/2800476/) by IYIKON, from [the Noun Project](https://thenounproject.com).

*Disclaimer:* The author of this library is in no way affiliated to [JetBrains s.r.o.](https://www.jetbrains.com/) (the makers of ReSharper) other than being a satisfied cutomer.