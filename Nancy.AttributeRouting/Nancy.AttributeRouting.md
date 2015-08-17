# Nancy.AttributeRouting

## AttributeRoutingRegistration

##### Namespace

Nancy.AttributeRouting

##### Summary

*Inherit from parent.*

### CollectionTypeRegistrations `property`

##### Summary

*Inherit from parent.*

### InstanceRegistrations `property`

##### Summary

*Inherit from parent.*

### TypeRegistrations `property`

##### Summary

*Inherit from parent.*

## AttributeRoutingResolver

##### Namespace

Nancy.AttributeRouting

##### Summary

The class to resolve routing attributes.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `AttributeRoutingResolver` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | Nancy.TinyIoc.TinyIoCContainer | The Nancy IoC container. |

## BeforeAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

Before attribute provides a hook to execute before enter the view model execution.

### Process `method`

##### Summary

Process the custom code and determine whether continue on view model execution.

##### Returns

The response. If this is `null`, it will continue on view model execution, otherwise it returns the this value directly.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | Nancy.TinyIoc.TinyIoCContainer | The Tiny IoC container. It provides `IUrlBuilder` and others to construct the response. |
| context | Nancy.NancyContext | The Nancy context. It provides user information and others to determine whether continue view model execution. |

## DeleteAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The Delete attribute. It indicates that this method hit with HTTP DELETE method.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `DeleteAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | System.String | The path to register into routing table. |

## DuplicatedRoutingPathsException

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

`DuplicatedRoutingPathsException` indicates two or more methods are decorated with a same routing path on a same HTTP method.

## GetAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The Get attribute. It indicates that this method hit with HTTP GET method.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `GetAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | System.String | The path to register into routing table. |

## IUrlBuilder

##### Namespace

Nancy.AttributeRouting

##### Summary

The URL builder interface. It accepts an expression related to a view model, then construct the URL string corresponding to the view model call.

### GetUrl\`\`1 `method`

##### Summary

Gets URL from the method call of a `T` instance.

##### Returns

The constructed URL string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | System.Linq.Expressions.Expression{System.Func{\`\`0,System.Object}} | The method call lambda expression from a `T` instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A view model class decorates with routing attributes. |

### GetUrl\`\`1 `method`

##### Summary

Gets URL from the method call of a `T` instance.

##### Returns

The constructed URL string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | System.Linq.Expressions.Expression{System.Func{\`\`0,System.Object}} | The method call lambda expression from a `T` instance. |
| parameters | System.Collections.Generic.IDictionary{System.String,System.String} | The parameter dictionary to provide additional information to construct the URL. The dictionary key is the routing template placeholder, the dictionary value is the actual value to replace the placeholder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A view model class decorates with routing attributes. |

### GetUrl\`\`1 `method`

##### Summary

Gets URL from the method call of a `T` instance.

##### Returns

The constructed URL string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | System.Linq.Expressions.Expression{System.Func{\`\`0,System.Object}} | The method call lambda expression from a `T` instance. |
| parameters | System.Object | The parameter object to provide additional information to construct the URL. The object will be converted into dictionary then invoke another overload. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A view model class decorates with routing attributes. |

## MultipleBeforeAttributeException

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

`MultipleBeforeAttributeException` indicates multiple before attributes are decorated on method or type.

## MultipleRouteAttributesException

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

`MultipleRouteAttributesException` indicates multiple route attributes are decorated on method.

## NoRouteAttributeException

##### Namespace

Nancy.AttributeRouting.Exceptions

##### Summary

`NoRouteAttributeException` indicates no route attribute is decorated on method.

## OptionsAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The Options attribute. It indicates that this method hit with HTTP OPTIONS method.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `OptionsAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | System.String | The path to register into routing table. |

## PatchAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The Patch attribute. It indicates that this method hit with HTTP PATCH method.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `PatchAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | System.String | The path to register into routing table. |

## PostAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The Post attribute. It indicates that this method hit with HTTP POST method.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `PostAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | System.String | The path to register into routing table. |

## PutAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The Put attribute. It indicates that this method hit with HTTP PUT method.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `PutAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | System.String | The path to register into routing table. |

## RequiresAnyClaimAttribute

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with `RequiresAnyClaimAttribute` indicates it requires authentication and any one of certain claims to be present.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `RequiresAnyClaimAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| requiredClaims | System.Collections.Generic.IEnumerable{System.String} | The claims to be present for authentication. |

### #ctor `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

### Process `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

## RequiresAuthenticationAttribute

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with `RequiresAuthenticationAttribute` indicates it requires authentication.

### Process `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

## RequiresClaimsAttribute

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with `RequiresClaimsAttribute` indicates it requires authentication and certain claims to be present.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `RequiresClaimsAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| claims | System.Collections.Generic.IEnumerable{System.String} | The claims to be present for authentication. |

### #ctor `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

### Process `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

## RequiresHttpsAttribute

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with `RequiresHttpsAttribute` indicates it requires HTTPS protocol.

### #ctor `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

### #ctor `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `RequiresHttpsAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| redirect | System.Boolean | True if the user should be redirected to HTTPS if the incoming request was made using HTTP, otherwise false if `Forbidden` should be returned. |
| httpsPort | System.Int32 | The HTTPS port number to use |

### Process `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

## RequiresValidatedClaimsAttribute

##### Namespace

Nancy.AttributeRouting.Security

##### Summary

The member decorated with `RequiresValidatedClaimsAttribute` indicates it requires claims to be validated.

### IsValid `method`

##### Summary

The implementation to validate claims.

##### Returns

True if the claims is valid, otherwise false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| claims | System.Collections.Generic.IEnumerable{System.String} | The claims from request. |

### Process `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

## RouteAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The Route attribute indicates the routing path to handle the request.

## RouteInheritAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

`RouteInheritAttribute` indicates a type inherit another type's routing information, including routing prefix, view prefix and before hooks.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `RouteInheritAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | System.Type | The type to inherit its routing information. |

## RoutePrefixAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The RoutePrefix attribute. It decorates on class, indicates the path from route attribute on the class and child class will be prefixed.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `RoutePrefixAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| prefix | System.String | The prefix string for the route attribute path. |

## UrlBuilder

##### Namespace

Nancy.AttributeRouting

##### Summary

*Inherit from parent.*

### #ctor `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

### GetUrl\`\`1 `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

### GetUrl\`\`1 `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

### GetUrl\`\`1 `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

## ViewAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The View attribute indicates the view path to render from request.

##### Example

The following code will render `View/index.html` with routing instance.

```
View('View/index.html')
```

### #ctor `constructor`

##### Summary

Initializes a new instance of the `ViewAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | System.String | The view path for rendering. |

## ViewPrefixAttribute

##### Namespace

Nancy.AttributeRouting

##### Summary

The ViewPrefix attribute. It decorates on class, indicates the View attribute works with this prefix to locate paths.

### #ctor `constructor`

##### Summary

Initializes a new instance of the `ViewPrefixAttribute` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| prefix | System.String | The path prefix. |
