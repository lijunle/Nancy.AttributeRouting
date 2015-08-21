<a name='contents'></a>
# Contents [#](#contents 'Go To Here')

- [AttributeRoutingRegistration](#T-Nancy.AttributeRouting.AttributeRoutingRegistration 'Nancy.AttributeRouting.AttributeRoutingRegistration')
  - [#ctor(typeProvider)](#M-Nancy.AttributeRouting.AttributeRoutingRegistration.#ctor-Nancy.AttributeRouting.ITypeProvider- 'Nancy.AttributeRouting.AttributeRoutingRegistration.#ctor(Nancy.AttributeRouting.ITypeProvider)')
  - [CollectionTypeRegistrations](#P-Nancy.AttributeRouting.AttributeRoutingRegistration.CollectionTypeRegistrations 'Nancy.AttributeRouting.AttributeRoutingRegistration.CollectionTypeRegistrations')
  - [InstanceRegistrations](#P-Nancy.AttributeRouting.AttributeRoutingRegistration.InstanceRegistrations 'Nancy.AttributeRouting.AttributeRoutingRegistration.InstanceRegistrations')
  - [TypeRegistrations](#P-Nancy.AttributeRouting.AttributeRoutingRegistration.TypeRegistrations 'Nancy.AttributeRouting.AttributeRoutingRegistration.TypeRegistrations')
- [AttributeRoutingResolver](#T-Nancy.AttributeRouting.AttributeRoutingResolver 'Nancy.AttributeRouting.AttributeRoutingResolver')
  - [#ctor(container)](#M-Nancy.AttributeRouting.AttributeRoutingResolver.#ctor-Nancy.TinyIoc.TinyIoCContainer- 'Nancy.AttributeRouting.AttributeRoutingResolver.#ctor(Nancy.TinyIoc.TinyIoCContainer)')
- [BeforeAttribute](#T-Nancy.AttributeRouting.BeforeAttribute 'Nancy.AttributeRouting.BeforeAttribute')
  - [Process(container,context)](#M-Nancy.AttributeRouting.BeforeAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Nancy.AttributeRouting.BeforeAttribute.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)')
- [DefaultTypeProvider](#T-Nancy.AttributeRouting.DefaultTypeProvider 'Nancy.AttributeRouting.DefaultTypeProvider')
  - [Types](#P-Nancy.AttributeRouting.DefaultTypeProvider.Types 'Nancy.AttributeRouting.DefaultTypeProvider.Types')
- [DeleteAttribute](#T-Nancy.AttributeRouting.DeleteAttribute 'Nancy.AttributeRouting.DeleteAttribute')
  - [#ctor(path)](#M-Nancy.AttributeRouting.DeleteAttribute.#ctor-System.String- 'Nancy.AttributeRouting.DeleteAttribute.#ctor(System.String)')
- [DuplicatedRoutingPathsException](#T-Nancy.AttributeRouting.Exceptions.DuplicatedRoutingPathsException 'Nancy.AttributeRouting.Exceptions.DuplicatedRoutingPathsException')
- [GetAttribute](#T-Nancy.AttributeRouting.GetAttribute 'Nancy.AttributeRouting.GetAttribute')
  - [#ctor(path)](#M-Nancy.AttributeRouting.GetAttribute.#ctor-System.String- 'Nancy.AttributeRouting.GetAttribute.#ctor(System.String)')
- [ITypeProvider](#T-Nancy.AttributeRouting.ITypeProvider 'Nancy.AttributeRouting.ITypeProvider')
  - [Types](#P-Nancy.AttributeRouting.ITypeProvider.Types 'Nancy.AttributeRouting.ITypeProvider.Types')
- [IUrlBuilder](#T-Nancy.AttributeRouting.IUrlBuilder 'Nancy.AttributeRouting.IUrlBuilder')
  - [GetUrl\`\`1(expression)](#M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}}- 'Nancy.AttributeRouting.IUrlBuilder.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})')
  - [GetUrl\`\`1(expression,parameters)](#M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object- 'Nancy.AttributeRouting.IUrlBuilder.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object)')
  - [GetUrl\`\`1(expression,parameters)](#M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String}- 'Nancy.AttributeRouting.IUrlBuilder.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String})')
- [MultipleBeforeAttributeException](#T-Nancy.AttributeRouting.Exceptions.MultipleBeforeAttributeException 'Nancy.AttributeRouting.Exceptions.MultipleBeforeAttributeException')
- [MultipleRouteAttributesException](#T-Nancy.AttributeRouting.Exceptions.MultipleRouteAttributesException 'Nancy.AttributeRouting.Exceptions.MultipleRouteAttributesException')
- [NoRouteAttributeException](#T-Nancy.AttributeRouting.Exceptions.NoRouteAttributeException 'Nancy.AttributeRouting.Exceptions.NoRouteAttributeException')
- [OptionsAttribute](#T-Nancy.AttributeRouting.OptionsAttribute 'Nancy.AttributeRouting.OptionsAttribute')
  - [#ctor(path)](#M-Nancy.AttributeRouting.OptionsAttribute.#ctor-System.String- 'Nancy.AttributeRouting.OptionsAttribute.#ctor(System.String)')
- [PatchAttribute](#T-Nancy.AttributeRouting.PatchAttribute 'Nancy.AttributeRouting.PatchAttribute')
  - [#ctor(path)](#M-Nancy.AttributeRouting.PatchAttribute.#ctor-System.String- 'Nancy.AttributeRouting.PatchAttribute.#ctor(System.String)')
- [PostAttribute](#T-Nancy.AttributeRouting.PostAttribute 'Nancy.AttributeRouting.PostAttribute')
  - [#ctor(path)](#M-Nancy.AttributeRouting.PostAttribute.#ctor-System.String- 'Nancy.AttributeRouting.PostAttribute.#ctor(System.String)')
- [PutAttribute](#T-Nancy.AttributeRouting.PutAttribute 'Nancy.AttributeRouting.PutAttribute')
  - [#ctor(path)](#M-Nancy.AttributeRouting.PutAttribute.#ctor-System.String- 'Nancy.AttributeRouting.PutAttribute.#ctor(System.String)')
- [RequiresAnyClaimAttribute](#T-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute 'Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute')
  - [#ctor(requiredClaims)](#M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor-System.Collections.Generic.IEnumerable{System.String}- 'Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor(System.Collections.Generic.IEnumerable{System.String})')
  - [#ctor()](#M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor-System.String[]- 'Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor(System.String[])')
  - [Process()](#M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)')
- [RequiresAuthenticationAttribute](#T-Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute 'Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute')
  - [Process()](#M-Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)')
- [RequiresClaimsAttribute](#T-Nancy.AttributeRouting.Security.RequiresClaimsAttribute 'Nancy.AttributeRouting.Security.RequiresClaimsAttribute')
  - [#ctor(claims)](#M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor-System.Collections.Generic.IEnumerable{System.String}- 'Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor(System.Collections.Generic.IEnumerable{System.String})')
  - [#ctor()](#M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor-System.String[]- 'Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor(System.String[])')
  - [Process()](#M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Nancy.AttributeRouting.Security.RequiresClaimsAttribute.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)')
- [RequiresHttpsAttribute](#T-Nancy.AttributeRouting.Security.RequiresHttpsAttribute 'Nancy.AttributeRouting.Security.RequiresHttpsAttribute')
  - [#ctor()](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor 'Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor')
  - [#ctor()](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor-System.Boolean- 'Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor(System.Boolean)')
  - [#ctor(redirect,httpsPort)](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor-System.Boolean,System.Int32- 'Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor(System.Boolean,System.Int32)')
  - [Process()](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Nancy.AttributeRouting.Security.RequiresHttpsAttribute.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)')
- [RequiresValidatedClaimsAttribute](#T-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute 'Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute')
  - [IsValid(claims)](#M-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.IsValid-System.Collections.Generic.IEnumerable{System.String}- 'Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.IsValid(System.Collections.Generic.IEnumerable{System.String})')
  - [Process()](#M-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)')
- [RouteAttribute](#T-Nancy.AttributeRouting.RouteAttribute 'Nancy.AttributeRouting.RouteAttribute')
- [RouteInheritAttribute](#T-Nancy.AttributeRouting.RouteInheritAttribute 'Nancy.AttributeRouting.RouteInheritAttribute')
  - [#ctor(type)](#M-Nancy.AttributeRouting.RouteInheritAttribute.#ctor-System.Type- 'Nancy.AttributeRouting.RouteInheritAttribute.#ctor(System.Type)')
- [RoutePrefixAttribute](#T-Nancy.AttributeRouting.RoutePrefixAttribute 'Nancy.AttributeRouting.RoutePrefixAttribute')
  - [#ctor(prefix)](#M-Nancy.AttributeRouting.RoutePrefixAttribute.#ctor-System.String- 'Nancy.AttributeRouting.RoutePrefixAttribute.#ctor(System.String)')
- [UrlBuilder](#T-Nancy.AttributeRouting.UrlBuilder 'Nancy.AttributeRouting.UrlBuilder')
  - [#ctor()](#M-Nancy.AttributeRouting.UrlBuilder.#ctor-Nancy.Routing.IRouteSegmentExtractor- 'Nancy.AttributeRouting.UrlBuilder.#ctor(Nancy.Routing.IRouteSegmentExtractor)')
  - [GetUrl\`\`1()](#M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}}- 'Nancy.AttributeRouting.UrlBuilder.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})')
  - [GetUrl\`\`1()](#M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object- 'Nancy.AttributeRouting.UrlBuilder.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object)')
  - [GetUrl\`\`1()](#M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String}- 'Nancy.AttributeRouting.UrlBuilder.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String})')
- [ViewAttribute](#T-Nancy.AttributeRouting.ViewAttribute 'Nancy.AttributeRouting.ViewAttribute')
  - [#ctor(path)](#M-Nancy.AttributeRouting.ViewAttribute.#ctor-System.String- 'Nancy.AttributeRouting.ViewAttribute.#ctor(System.String)')
- [ViewPrefixAttribute](#T-Nancy.AttributeRouting.ViewPrefixAttribute 'Nancy.AttributeRouting.ViewPrefixAttribute')
  - [#ctor(prefix)](#M-Nancy.AttributeRouting.ViewPrefixAttribute.#ctor-System.String- 'Nancy.AttributeRouting.ViewPrefixAttribute.#ctor(System.String)')

<a name='assembly'></a>
# Nancy.AttributeRouting [#](#assembly 'Go To Here') [^](#contents 'Back To Contents')

<a name='T-Nancy.AttributeRouting.AttributeRoutingRegistration'></a>
## AttributeRoutingRegistration [#](#T-Nancy.AttributeRouting.AttributeRoutingRegistration 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

*Inherit from parent.*

<a name='M-Nancy.AttributeRouting.AttributeRoutingRegistration.#ctor-Nancy.AttributeRouting.ITypeProvider-'></a>
### #ctor(typeProvider) `constructor` [#](#M-Nancy.AttributeRouting.AttributeRoutingRegistration.#ctor-Nancy.AttributeRouting.ITypeProvider- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [AttributeRoutingRegistration](#T-Nancy.AttributeRouting.AttributeRoutingRegistration 'Nancy.AttributeRouting.AttributeRoutingRegistration') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| typeProvider | [Nancy.AttributeRouting.ITypeProvider](#T-Nancy.AttributeRouting.ITypeProvider 'Nancy.AttributeRouting.ITypeProvider') | The routing type provider. |

<a name='P-Nancy.AttributeRouting.AttributeRoutingRegistration.CollectionTypeRegistrations'></a>
### CollectionTypeRegistrations `property` [#](#P-Nancy.AttributeRouting.AttributeRoutingRegistration.CollectionTypeRegistrations 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

<a name='P-Nancy.AttributeRouting.AttributeRoutingRegistration.InstanceRegistrations'></a>
### InstanceRegistrations `property` [#](#P-Nancy.AttributeRouting.AttributeRoutingRegistration.InstanceRegistrations 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

<a name='P-Nancy.AttributeRouting.AttributeRoutingRegistration.TypeRegistrations'></a>
### TypeRegistrations `property` [#](#P-Nancy.AttributeRouting.AttributeRoutingRegistration.TypeRegistrations 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

<a name='T-Nancy.AttributeRouting.AttributeRoutingResolver'></a>
## AttributeRoutingResolver [#](#T-Nancy.AttributeRouting.AttributeRoutingResolver 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The class to resolve routing attributes.

<a name='M-Nancy.AttributeRouting.AttributeRoutingResolver.#ctor-Nancy.TinyIoc.TinyIoCContainer-'></a>
### #ctor(container) `constructor` [#](#M-Nancy.AttributeRouting.AttributeRoutingResolver.#ctor-Nancy.TinyIoc.TinyIoCContainer- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [AttributeRoutingResolver](#T-Nancy.AttributeRouting.AttributeRoutingResolver 'Nancy.AttributeRouting.AttributeRoutingResolver') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Nancy.TinyIoc.TinyIoCContainer](#T-Nancy.TinyIoc.TinyIoCContainer 'Nancy.TinyIoc.TinyIoCContainer') | The Nancy IoC container. |

<a name='T-Nancy.AttributeRouting.BeforeAttribute'></a>
## BeforeAttribute [#](#T-Nancy.AttributeRouting.BeforeAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

Before attribute provides a hook to execute before enter the view model execution.

<a name='M-Nancy.AttributeRouting.BeforeAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext-'></a>
### Process(container,context) `method` [#](#M-Nancy.AttributeRouting.BeforeAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Process the custom code and determine whether continue on view model execution.

##### Returns

The response. If this is `null`, it will continue on view model execution, otherwise it returns the this value directly.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Nancy.TinyIoc.TinyIoCContainer](#T-Nancy.TinyIoc.TinyIoCContainer 'Nancy.TinyIoc.TinyIoCContainer') | The Tiny IoC container. It provides [IUrlBuilder](#T-Nancy.AttributeRouting.IUrlBuilder 'Nancy.AttributeRouting.IUrlBuilder') and others to construct the response. |
| context | [Nancy.NancyContext](#T-Nancy.NancyContext 'Nancy.NancyContext') | The Nancy context. It provides user information and others to determine whether continue view model execution. |

<a name='T-Nancy.AttributeRouting.DefaultTypeProvider'></a>
## DefaultTypeProvider [#](#T-Nancy.AttributeRouting.DefaultTypeProvider 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The default type provider for routing register.

<a name='P-Nancy.AttributeRouting.DefaultTypeProvider.Types'></a>
### Types `property` [#](#P-Nancy.AttributeRouting.DefaultTypeProvider.Types 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

<a name='T-Nancy.AttributeRouting.DeleteAttribute'></a>
## DeleteAttribute [#](#T-Nancy.AttributeRouting.DeleteAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The Delete attribute. It indicates that this method hit with HTTP DELETE method.

<a name='M-Nancy.AttributeRouting.DeleteAttribute.#ctor-System.String-'></a>
### #ctor(path) `constructor` [#](#M-Nancy.AttributeRouting.DeleteAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [DeleteAttribute](#T-Nancy.AttributeRouting.DeleteAttribute 'Nancy.AttributeRouting.DeleteAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path to register into routing table. |

<a name='T-Nancy.AttributeRouting.Exceptions.DuplicatedRoutingPathsException'></a>
## DuplicatedRoutingPathsException [#](#T-Nancy.AttributeRouting.Exceptions.DuplicatedRoutingPathsException 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

[DuplicatedRoutingPathsException](#T-Nancy.AttributeRouting.Exceptions.DuplicatedRoutingPathsException 'Nancy.AttributeRouting.Exceptions.DuplicatedRoutingPathsException') indicates two or more methods are decorated with a same routing path on a same HTTP method.

<a name='T-Nancy.AttributeRouting.GetAttribute'></a>
## GetAttribute [#](#T-Nancy.AttributeRouting.GetAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The Get attribute. It indicates that this method hit with HTTP GET method.

<a name='M-Nancy.AttributeRouting.GetAttribute.#ctor-System.String-'></a>
### #ctor(path) `constructor` [#](#M-Nancy.AttributeRouting.GetAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [GetAttribute](#T-Nancy.AttributeRouting.GetAttribute 'Nancy.AttributeRouting.GetAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path to register into routing table. |

<a name='T-Nancy.AttributeRouting.ITypeProvider'></a>
## ITypeProvider [#](#T-Nancy.AttributeRouting.ITypeProvider 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

Type provider to provide types for routing register.

<a name='P-Nancy.AttributeRouting.ITypeProvider.Types'></a>
### Types `property` [#](#P-Nancy.AttributeRouting.ITypeProvider.Types 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Gets the type list for routing register. Their methods decorated with [RouteAttribute](#T-Nancy.AttributeRouting.RouteAttribute 'Nancy.AttributeRouting.RouteAttribute') will be registed to routing table. By default, all types will be registed.

<a name='T-Nancy.AttributeRouting.IUrlBuilder'></a>
## IUrlBuilder [#](#T-Nancy.AttributeRouting.IUrlBuilder 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The URL builder interface. It accepts an expression related to a view model, then construct the URL string corresponding to the view model call.

<a name='M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}}-'></a>
### GetUrl\`\`1(expression) `method` [#](#M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}}- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Gets URL from the method call of a `T` instance.

##### Returns

The constructed URL string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | [System.Linq.Expressions.Expression{System.Func{\`\`0,System.Object}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{``0,System.Object}}') | The method call lambda expression from a `T` instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A view model class decorates with routing attributes. |

<a name='M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object-'></a>
### GetUrl\`\`1(expression,parameters) `method` [#](#M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Gets URL from the method call of a `T` instance.

##### Returns

The constructed URL string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | [System.Linq.Expressions.Expression{System.Func{\`\`0,System.Object}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{``0,System.Object}}') | The method call lambda expression from a `T` instance. |
| parameters | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The parameter object to provide additional information to construct the URL. The object will be converted into dictionary then invoke another overload. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A view model class decorates with routing attributes. |

<a name='M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String}-'></a>
### GetUrl\`\`1(expression,parameters) `method` [#](#M-Nancy.AttributeRouting.IUrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String}- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Gets URL from the method call of a `T` instance.

##### Returns

The constructed URL string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | [System.Linq.Expressions.Expression{System.Func{\`\`0,System.Object}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{``0,System.Object}}') | The method call lambda expression from a `T` instance. |
| parameters | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The parameter dictionary to provide additional information to construct the URL. The dictionary key is the routing template placeholder, the dictionary value is the actual value to replace the placeholder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A view model class decorates with routing attributes. |

<a name='T-Nancy.AttributeRouting.Exceptions.MultipleBeforeAttributeException'></a>
## MultipleBeforeAttributeException [#](#T-Nancy.AttributeRouting.Exceptions.MultipleBeforeAttributeException 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

[MultipleBeforeAttributeException](#T-Nancy.AttributeRouting.Exceptions.MultipleBeforeAttributeException 'Nancy.AttributeRouting.Exceptions.MultipleBeforeAttributeException') indicates multiple before attributes are decorated on method or type.

<a name='T-Nancy.AttributeRouting.Exceptions.MultipleRouteAttributesException'></a>
## MultipleRouteAttributesException [#](#T-Nancy.AttributeRouting.Exceptions.MultipleRouteAttributesException 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

[MultipleRouteAttributesException](#T-Nancy.AttributeRouting.Exceptions.MultipleRouteAttributesException 'Nancy.AttributeRouting.Exceptions.MultipleRouteAttributesException') indicates multiple route attributes are decorated on method.

<a name='T-Nancy.AttributeRouting.Exceptions.NoRouteAttributeException'></a>
## NoRouteAttributeException [#](#T-Nancy.AttributeRouting.Exceptions.NoRouteAttributeException 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

[NoRouteAttributeException](#T-Nancy.AttributeRouting.Exceptions.NoRouteAttributeException 'Nancy.AttributeRouting.Exceptions.NoRouteAttributeException') indicates no route attribute is decorated on method.

<a name='T-Nancy.AttributeRouting.OptionsAttribute'></a>
## OptionsAttribute [#](#T-Nancy.AttributeRouting.OptionsAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The Options attribute. It indicates that this method hit with HTTP OPTIONS method.

<a name='M-Nancy.AttributeRouting.OptionsAttribute.#ctor-System.String-'></a>
### #ctor(path) `constructor` [#](#M-Nancy.AttributeRouting.OptionsAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [OptionsAttribute](#T-Nancy.AttributeRouting.OptionsAttribute 'Nancy.AttributeRouting.OptionsAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path to register into routing table. |

<a name='T-Nancy.AttributeRouting.PatchAttribute'></a>
## PatchAttribute [#](#T-Nancy.AttributeRouting.PatchAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The Patch attribute. It indicates that this method hit with HTTP PATCH method.

<a name='M-Nancy.AttributeRouting.PatchAttribute.#ctor-System.String-'></a>
### #ctor(path) `constructor` [#](#M-Nancy.AttributeRouting.PatchAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PatchAttribute](#T-Nancy.AttributeRouting.PatchAttribute 'Nancy.AttributeRouting.PatchAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path to register into routing table. |

<a name='T-Nancy.AttributeRouting.PostAttribute'></a>
## PostAttribute [#](#T-Nancy.AttributeRouting.PostAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The Post attribute. It indicates that this method hit with HTTP POST method.

<a name='M-Nancy.AttributeRouting.PostAttribute.#ctor-System.String-'></a>
### #ctor(path) `constructor` [#](#M-Nancy.AttributeRouting.PostAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PostAttribute](#T-Nancy.AttributeRouting.PostAttribute 'Nancy.AttributeRouting.PostAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path to register into routing table. |

<a name='T-Nancy.AttributeRouting.PutAttribute'></a>
## PutAttribute [#](#T-Nancy.AttributeRouting.PutAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The Put attribute. It indicates that this method hit with HTTP PUT method.

<a name='M-Nancy.AttributeRouting.PutAttribute.#ctor-System.String-'></a>
### #ctor(path) `constructor` [#](#M-Nancy.AttributeRouting.PutAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PutAttribute](#T-Nancy.AttributeRouting.PutAttribute 'Nancy.AttributeRouting.PutAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path to register into routing table. |

<a name='T-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute'></a>
## RequiresAnyClaimAttribute [#](#T-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with [RequiresAnyClaimAttribute](#T-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute 'Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute') indicates it requires authentication and any one of certain claims to be present.

<a name='M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor-System.Collections.Generic.IEnumerable{System.String}-'></a>
### #ctor(requiredClaims) `constructor` [#](#M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor-System.Collections.Generic.IEnumerable{System.String}- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [RequiresAnyClaimAttribute](#T-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute 'Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| requiredClaims | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | The claims to be present for authentication. |

<a name='M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor-System.String[]-'></a>
### #ctor() `constructor` [#](#M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.#ctor-System.String[]- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext-'></a>
### Process() `method` [#](#M-Nancy.AttributeRouting.Security.RequiresAnyClaimAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute'></a>
## RequiresAuthenticationAttribute [#](#T-Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with [RequiresAuthenticationAttribute](#T-Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute 'Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute') indicates it requires authentication.

<a name='M-Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext-'></a>
### Process() `method` [#](#M-Nancy.AttributeRouting.Security.RequiresAuthenticationAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Nancy.AttributeRouting.Security.RequiresClaimsAttribute'></a>
## RequiresClaimsAttribute [#](#T-Nancy.AttributeRouting.Security.RequiresClaimsAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with [RequiresClaimsAttribute](#T-Nancy.AttributeRouting.Security.RequiresClaimsAttribute 'Nancy.AttributeRouting.Security.RequiresClaimsAttribute') indicates it requires authentication and certain claims to be present.

<a name='M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor-System.Collections.Generic.IEnumerable{System.String}-'></a>
### #ctor(claims) `constructor` [#](#M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor-System.Collections.Generic.IEnumerable{System.String}- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [RequiresClaimsAttribute](#T-Nancy.AttributeRouting.Security.RequiresClaimsAttribute 'Nancy.AttributeRouting.Security.RequiresClaimsAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| claims | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | The claims to be present for authentication. |

<a name='M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor-System.String[]-'></a>
### #ctor() `constructor` [#](#M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.#ctor-System.String[]- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext-'></a>
### Process() `method` [#](#M-Nancy.AttributeRouting.Security.RequiresClaimsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Nancy.AttributeRouting.Security.RequiresHttpsAttribute'></a>
## RequiresHttpsAttribute [#](#T-Nancy.AttributeRouting.Security.RequiresHttpsAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with [RequiresHttpsAttribute](#T-Nancy.AttributeRouting.Security.RequiresHttpsAttribute 'Nancy.AttributeRouting.Security.RequiresHttpsAttribute') indicates it requires HTTPS protocol.

<a name='M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor'></a>
### #ctor() `constructor` [#](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor-System.Boolean-'></a>
### #ctor() `constructor` [#](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor-System.Boolean- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor-System.Boolean,System.Int32-'></a>
### #ctor(redirect,httpsPort) `constructor` [#](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.#ctor-System.Boolean,System.Int32- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [RequiresHttpsAttribute](#T-Nancy.AttributeRouting.Security.RequiresHttpsAttribute 'Nancy.AttributeRouting.Security.RequiresHttpsAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| redirect | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | True if the user should be redirected to HTTPS if the incoming request was made using HTTP, otherwise false if [Forbidden](#F-Nancy.HttpStatusCode.Forbidden 'Nancy.HttpStatusCode.Forbidden') should be returned. |
| httpsPort | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The HTTPS port number to use |

<a name='M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext-'></a>
### Process() `method` [#](#M-Nancy.AttributeRouting.Security.RequiresHttpsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute'></a>
## RequiresValidatedClaimsAttribute [#](#T-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with [RequiresValidatedClaimsAttribute](#T-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute 'Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute') indicates it requires claims to be validated.

<a name='M-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.IsValid-System.Collections.Generic.IEnumerable{System.String}-'></a>
### IsValid(claims) `method` [#](#M-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.IsValid-System.Collections.Generic.IEnumerable{System.String}- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

The implementation to validate claims.

##### Returns

True if the claims is valid, otherwise false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| claims | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | The claims from request. |

<a name='M-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext-'></a>
### Process() `method` [#](#M-Nancy.AttributeRouting.Security.RequiresValidatedClaimsAttribute.Process-Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Nancy.AttributeRouting.RouteAttribute'></a>
## RouteAttribute [#](#T-Nancy.AttributeRouting.RouteAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The Route attribute indicates the routing path to handle the request.

<a name='T-Nancy.AttributeRouting.RouteInheritAttribute'></a>
## RouteInheritAttribute [#](#T-Nancy.AttributeRouting.RouteInheritAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

[RouteInheritAttribute](#T-Nancy.AttributeRouting.RouteInheritAttribute 'Nancy.AttributeRouting.RouteInheritAttribute') indicates a type inherit another type's routing information, including routing prefix, view prefix and before hooks.

<a name='M-Nancy.AttributeRouting.RouteInheritAttribute.#ctor-System.Type-'></a>
### #ctor(type) `constructor` [#](#M-Nancy.AttributeRouting.RouteInheritAttribute.#ctor-System.Type- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [RouteInheritAttribute](#T-Nancy.AttributeRouting.RouteInheritAttribute 'Nancy.AttributeRouting.RouteInheritAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type to inherit its routing information. |

<a name='T-Nancy.AttributeRouting.RoutePrefixAttribute'></a>
## RoutePrefixAttribute [#](#T-Nancy.AttributeRouting.RoutePrefixAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The RoutePrefix attribute. It decorates on class, indicates the path from route attribute on the class and child class will be prefixed.

<a name='M-Nancy.AttributeRouting.RoutePrefixAttribute.#ctor-System.String-'></a>
### #ctor(prefix) `constructor` [#](#M-Nancy.AttributeRouting.RoutePrefixAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [RoutePrefixAttribute](#T-Nancy.AttributeRouting.RoutePrefixAttribute 'Nancy.AttributeRouting.RoutePrefixAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| prefix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The prefix string for the route attribute path. |

<a name='T-Nancy.AttributeRouting.UrlBuilder'></a>
## UrlBuilder [#](#T-Nancy.AttributeRouting.UrlBuilder 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

*Inherit from parent.*

<a name='M-Nancy.AttributeRouting.UrlBuilder.#ctor-Nancy.Routing.IRouteSegmentExtractor-'></a>
### #ctor() `constructor` [#](#M-Nancy.AttributeRouting.UrlBuilder.#ctor-Nancy.Routing.IRouteSegmentExtractor- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}}-'></a>
### GetUrl\`\`1() `method` [#](#M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}}- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object-'></a>
### GetUrl\`\`1() `method` [#](#M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String}-'></a>
### GetUrl\`\`1() `method` [#](#M-Nancy.AttributeRouting.UrlBuilder.GetUrl``1-System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String}- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Nancy.AttributeRouting.ViewAttribute'></a>
## ViewAttribute [#](#T-Nancy.AttributeRouting.ViewAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The View attribute indicates the view path to render from request.

##### Example

The following code will render `View/index.html` with routing instance.

```
View('View/index.html')
```

<a name='M-Nancy.AttributeRouting.ViewAttribute.#ctor-System.String-'></a>
### #ctor(path) `constructor` [#](#M-Nancy.AttributeRouting.ViewAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ViewAttribute](#T-Nancy.AttributeRouting.ViewAttribute 'Nancy.AttributeRouting.ViewAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The view path for rendering. |

<a name='T-Nancy.AttributeRouting.ViewPrefixAttribute'></a>
## ViewPrefixAttribute [#](#T-Nancy.AttributeRouting.ViewPrefixAttribute 'Go To Here') [^](#contents 'Back To Contents')

##### Namespace

Nancy.AttributeRouting

##### Summary

The ViewPrefix attribute. It decorates on class, indicates the View attribute works with this prefix to locate paths.

<a name='M-Nancy.AttributeRouting.ViewPrefixAttribute.#ctor-System.String-'></a>
### #ctor(prefix) `constructor` [#](#M-Nancy.AttributeRouting.ViewPrefixAttribute.#ctor-System.String- 'Go To Here') [^](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ViewPrefixAttribute](#T-Nancy.AttributeRouting.ViewPrefixAttribute 'Nancy.AttributeRouting.ViewPrefixAttribute') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| prefix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path prefix. |
