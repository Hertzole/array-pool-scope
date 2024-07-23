using System.Runtime.CompilerServices;

#if UNITY_64
[assembly: InternalsVisibleTo("Hertzole.ArrayPoolScope.Tests")]
#else
[assembly: InternalsVisibleTo("ArrayPoolScope.Tests")]
#endif