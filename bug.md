I run in to an expected test failure when I was substituting my `Dictionary<>` based cache.
The key I use for the dictionary is a composite of 2 values, and we know that the key object has to implement `IEquatable<>` in order to find the value in the dictionary again. This works:
`ICompositeKeyCache cache = new CompositeKeyCache();`
`cache.Add(new CompositeKey(Constants.KeyPart1, Constants.KeyPart2), Constants.ReturnValue);`
`string retVal = cache.Get(new CompositeKey(Constants.KeyPart1, Constants.KeyPart2));`

Unfortunately the substitute I used for testing this failed in this case:
`var cacheSub = Substitute.For<ICompositeKeyCache>();`
`cacheSub.Get(new CompositeKey(Constants.KeyPart1, Constants.KeyPart)).Returns(Constants.ReturnValue);`
`string retVal = cacheSub.Get(new CompositeKey(Constants.KeyPart1, Constants.KeyPart2));`
After some investigation I learned that in order for the substitute to work the key objects also has to override the `Equals()` method. Overriding the `Equals()` method also works in the actual dictionary.

Somewhat annoying when the test fails and actual code works. I think implementing `IEquatable<>` should also work with NSubstitute.


see complete code here:
[https://github.com/niklasda/NSubstituteTestCaseProject](https://github.com/niklasda/NSubstituteTestCaseProject)

Keep up the good work

