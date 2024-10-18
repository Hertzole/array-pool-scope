## [2.0.1](https://github.com/Hertzole/array-pool-scope/compare/v2.0.0...v2.0.1) (2024-10-18)


### Bug Fixes

* unity meta files not being generated ([d65dd56](https://github.com/Hertzole/array-pool-scope/commit/d65dd5678d5a355c821c23132d8863f66c190dbf))

# [2.0.0](https://github.com/Hertzole/array-pool-scope/compare/v1.1.0...v2.0.0) (2024-09-24)


### Bug Fixes

* not throwing ArgumentNullException in some methods ([d68ab96](https://github.com/Hertzole/array-pool-scope/commit/d68ab969c83fbe67a535fc0ca56ee4c1268b7c81))
* Sort with comparison sorting the entire array ([d974b16](https://github.com/Hertzole/array-pool-scope/commit/d974b16835105943a1f1ddd07718dfe472f69bf0))
* static random field in generic class ([9ac8fcd](https://github.com/Hertzole/array-pool-scope/commit/9ac8fcd055922bba4395af619807045102bfa700))
* xml documentation not being included (for real this time) ([5105a63](https://github.com/Hertzole/array-pool-scope/commit/5105a635261f14b043d6c99524fd63fe665c4417))


* refactor!: simplify constructors ([7dbe447](https://github.com/Hertzole/array-pool-scope/commit/7dbe447b51ef25042a17f02a643c78a8f6acacff))
* refactor!: change Count to Length ([eb36f35](https://github.com/Hertzole/array-pool-scope/commit/eb36f3521472e23b744e42ce63af25142f743cb3))


### Features

* constructor with span and memory ([f85cbe7](https://github.com/Hertzole/array-pool-scope/commit/f85cbe7d2ea1b2126353580781b187eb43c7cc31))
* CopyTo(Array) ([7b9fa50](https://github.com/Hertzole/array-pool-scope/commit/7b9fa5099a98a3f4746dc514fa51c2b9119729ca))
* rent extensions methods that take array, collection, span and memory ([9eeb254](https://github.com/Hertzole/array-pool-scope/commit/9eeb254f4fd9dfba29c22378ca6abbd6d653f9e8))
* sort method using Comparison ([402d487](https://github.com/Hertzole/array-pool-scope/commit/402d48763af8722fb9ecee8ba30d77405f2fe081))
* support .NET Standard 1.1 ([dd8ba97](https://github.com/Hertzole/array-pool-scope/commit/dd8ba9757e74a2544f12d7d0731df7017cac4c58))


### BREAKING CHANGES

* Constructors with a pool are no longer nullable as there are now separate constructors with pools.
* This is to make it more familiar to arrays.

# [1.1.0](https://github.com/Hertzole/array-pool-scope/compare/v1.0.0...v1.1.0) (2024-08-11)


### Bug Fixes

* being able to access elements outside of the array range ([0f3f2f8](https://github.com/Hertzole/array-pool-scope/commit/0f3f2f8de90e7889236652a3afc17b1b59c9c814))
* documentation not being included in NuGet package ([bc9ac53](https://github.com/Hertzole/array-pool-scope/commit/bc9ac539fa074e82a416ee30fcfda8f76a49dd97))
* license not being properly included ([2290956](https://github.com/Hertzole/array-pool-scope/commit/2290956f2ba9777cf22761d71890bd275d66afd8))


### Features

* Contains method ([a2f7d75](https://github.com/Hertzole/array-pool-scope/commit/a2f7d7534f6d8883b7a81a56d3981bec2290d86a))
* IndexOf method ([be96dbb](https://github.com/Hertzole/array-pool-scope/commit/be96dbb102a62e3f11197426b1774549ebc3a8e3))
* Reverse method ([8fb664e](https://github.com/Hertzole/array-pool-scope/commit/8fb664ed6473cb0ae9cb52bd134be86850e034c7))
* Shuffle method ([6883cc9](https://github.com/Hertzole/array-pool-scope/commit/6883cc94c95245c53d65ec2374d86f202449c494))
* Sort method ([3cf8af3](https://github.com/Hertzole/array-pool-scope/commit/3cf8af35b0141810c6094d62f0c559a323df9e65))
* TrueForAll method ([ece883f](https://github.com/Hertzole/array-pool-scope/commit/ece883f60f5c0d9fe18edb74c3bc20107a0c3d47))
* UnsafeArrayScope.GetArray for getting the internal array ([0e2b6cd](https://github.com/Hertzole/array-pool-scope/commit/0e2b6cd858e2e79bcf5b993f422088788cc99124))

# 1.0.0 (2024-07-23)


Initial release
