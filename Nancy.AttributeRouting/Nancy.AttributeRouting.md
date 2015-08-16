
# Nancy.AttributeRouting


## AttributeRoutingRegistration


### .CollectionTypeRegistrations


### .InstanceRegistrations


### .TypeRegistrations


## AttributeRoutingResolver

The class to resolve routing attributes.


### M:Nancy.AttributeRouting.#ctor(container)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| container | *Nancy.TinyIoc.TinyIoCContainer*<br>The Nancy IoC container. |

## BeforeAttribute

Before attribute provides a hook to execute before enter the view model execution.


### M:Nancy.AttributeRouting.Process(container, context)

Process the custom code and determine whether continue on view model execution.

| Name | Description |
| ---- | ----------- |
| container | *Nancy.TinyIoc.TinyIoCContainer*<br> The Tiny IoC container. It provides and others to construct the response.  |
| context | *Nancy.NancyContext*<br> The Nancy context. It provides user information and others to determine whether continue view model execution.  |


#### Returns

 The response. If this is null, it will continue on view model execution, otherwise it returns the this value directly. 


## DeleteAttribute

The Delete attribute. It indicates that this method hit with HTTP DELETE method.


### M:Nancy.AttributeRouting.#ctor(path)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The path to register into routing table. |

## Exceptions.DuplicatedRoutingPathsException

indicates two or more methods are decorated with a same routing path on a same HTTP method.


## Exceptions.MultipleBeforeAttributeException

indicates multiple before attributes are decorated on method or type.


## Exceptions.MultipleRouteAttributesException

indicates multiple route attributes are decorated on method.


## Exceptions.NoRouteAttributeException

indicates no route attribute is decorated on method.


## GetAttribute

The Get attribute. It indicates that this method hit with HTTP GET method.


### M:Nancy.AttributeRouting.#ctor(path)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The path to register into routing table. |

## IUrlBuilder

The URL builder interface. It accepts an expression related to a view model, then construct the URL string corresponding to the view model call.


### M:Nancy.AttributeRouting.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})

Gets URL from the method call of a instance.

| Name | Description |
| ---- | ----------- |
| expression | *Unknown type*<br> The method call lambda expression from a instance.  |


#### Returns

The constructed URL string.


### M:Nancy.AttributeRouting.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String})

Gets URL from the method call of a instance.

| Name | Description |
| ---- | ----------- |
| expression | *Unknown type*<br> The method call lambda expression from a instance.  |
| parameters | *Unknown type*<br> The parameter dictionary to provide additional information to construct the URL. The dictionary key is the routing template placeholder, the dictionary value is the actual value to replace the placeholder.  |


#### Returns

The constructed URL string.


### M:Nancy.AttributeRouting.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object)

Gets URL from the method call of a instance.

| Name | Description |
| ---- | ----------- |
| expression | *Unknown type*<br> The method call lambda expression from a instance.  |
| parameters | *Unknown type*<br> The parameter object to provide additional information to construct the URL. The object will be converted into dictionary then invoke another overload.  |


#### Returns

The constructed URL string.


## OptionsAttribute

The Options attribute. It indicates that this method hit with HTTP OPTIONS method.


### M:Nancy.AttributeRouting.#ctor(path)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The path to register into routing table. |

## PatchAttribute

The Patch attribute. It indicates that this method hit with HTTP PATCH method.


### M:Nancy.AttributeRouting.#ctor(path)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The path to register into routing table. |

## PostAttribute

The Post attribute. It indicates that this method hit with HTTP POST method.


### M:Nancy.AttributeRouting.#ctor(path)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The path to register into routing table. |

## PutAttribute

The Put attribute. It indicates that this method hit with HTTP PUT method.


### M:Nancy.AttributeRouting.#ctor(path)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The path to register into routing table. |

## RouteAttribute

The Route attribute indicates the routing path to handle the request.


## RouteInheritAttribute

indicates a type inherit another type's routing information, including routing prefix, view prefix and before hooks.


### M:Nancy.AttributeRouting.#ctor(type)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| type | *System.Type*<br>The type to inherit its routing information. |

## RoutePrefixAttribute

The RoutePrefix attribute. It decorates on class, indicates the path from route attribute on the class and child class will be prefixed.


### M:Nancy.AttributeRouting.#ctor(prefix)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| prefix | *System.String*<br>The prefix string for the route attribute path. |

## Security.RequiresAnyClaimAttribute

The member decorated with indicates it requires authentication and any one of certain claims to be present.


### M:Nancy.AttributeRouting.#ctor(requiredClaims)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| requiredClaims | *System.Collections.Generic.IEnumerable{System.String}*<br>The claims to be present for authentication. |

### M:Nancy.AttributeRouting.#ctor(System.String[])


### M:Nancy.AttributeRouting.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)


## Security.RequiresAuthenticationAttribute

The member decorated with indicates it requires authentication.


### M:Nancy.AttributeRouting.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)


## Security.RequiresClaimsAttribute

The member decorated with indicates it requires authentication and certain claims to be present.


### M:Nancy.AttributeRouting.#ctor(claims)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| claims | *System.Collections.Generic.IEnumerable{System.String}*<br>The claims to be present for authentication. |

### M:Nancy.AttributeRouting.#ctor(System.String[])


### M:Nancy.AttributeRouting.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)


## Security.RequiresHttpsAttribute

The member decorated with indicates it requires HTTPS protocol.


### M:Nancy.AttributeRouting.#ctor


### M:Nancy.AttributeRouting.#ctor(System.Boolean)


### M:Nancy.AttributeRouting.#ctor(redirect, httpsPort)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| redirect | *System.Boolean*<br> True if the user should be redirected to HTTPS if the incoming request was made using HTTP, otherwise false if should be returned.  |
| httpsPort | *System.Int32*<br>The HTTPS port number to use |

### M:Nancy.AttributeRouting.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)


## Security.RequiresValidatedClaimsAttribute

The member decorated with indicates it requires claims to be validated.


### M:Nancy.AttributeRouting.IsValid(claims)

The implementation to validate claims.

| Name | Description |
| ---- | ----------- |
| claims | *System.Collections.Generic.IEnumerable{System.String}*<br>The claims from request. |


#### Returns

True if the claims is valid, otherwise false.


### M:Nancy.AttributeRouting.Process(Nancy.TinyIoc.TinyIoCContainer,Nancy.NancyContext)


## UrlBuilder


### M:Nancy.AttributeRouting.#ctor(Nancy.Routing.IRouteSegmentExtractor)


### M:Nancy.AttributeRouting.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})


### M:Nancy.AttributeRouting.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Collections.Generic.IDictionary{System.String,System.String})


### M:Nancy.AttributeRouting.GetUrl``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Object)


## ViewAttribute

The View attribute indicates the view path to render from request.


### M:Nancy.AttributeRouting.#ctor(path)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The view path for rendering. |

## ViewPrefixAttribute

The ViewPrefix attribute. It decorates on class, indicates the View attribute works with this prefix to locate paths.


### M:Nancy.AttributeRouting.#ctor(prefix)

Initializes a new instance of the class.

| Name | Description |
| ---- | ----------- |
| prefix | *System.String*<br>The path prefix. |
