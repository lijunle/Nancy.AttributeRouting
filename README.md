# Nancy.AttributeRouting

[![Build status](https://ci.appveyor.com/api/projects/status/86jwerymo30f9949/branch/master?svg=true)](https://ci.appveyor.com/project/lijunle/nancy-attributerouting/branch/master)
[![Coverage Status](https://coveralls.io/repos/lijunle/Nancy.AttributeRouting/badge.svg?branch=master&service=github)](https://coveralls.io/github/lijunle/Nancy.AttributeRouting?branch=master)

Enable attribute routing for Nancy project, and build route URL with compiled-time checked lambda expression.

# Install

```powershell
PM> Install-Package Nancy.AttributeRouting
```

# Why to use AttributeRouting?

With AttributeRouting, there is no need to write [NancyModule](https://github.com/NancyFx/Nancy/wiki/Exploring-the-nancy-module), [route](https://github.com/NancyFx/Nancy/wiki/Defining-routes) and [content negotiate](https://github.com/NancyFx/Nancy/wiki/Content-Negotiation) are handled by attributes.

With AttributeRouting, you can write a clean MVVM (Model - View - View Model) approach with Nancy framework.

# Usage Tutorial

The tutorial is to leverage AttributeRouting to build two simple pages.

## Todo List Page

The first page to list some todo items in our `/todo` page.

The following are the corresponding view and view model. (*Note:* you can use any view engine, I use the default [Super Simple View Engine](https://github.com/NancyFx/Nancy/wiki/The-Super-Simple-View-Engine) in my example.)

```html
<!-- todo view, place as `/Views/Todo.html` -->
<html>
    <body>
        <ul>
        @Each.Items
            <li>@Current</li>
        @EndEach
        </ul>
    </body>
</html>
```

```csharp
// todo view model, place as `/ViewModels/Todo.cs`
public class Todo
{
    [Get("/todo")]
    [View("Todo")]
    public Todo(IDatabase database) // Use IoC to inject dependencies to view model
    {
        this.Items = database.TodoItems; // get items from database or external data sources
    }

    public List<string> Items { get; set; }
}
```

Notice the `Get` and `View` attribute on `Todo` class constructor:

- The `Get` attribute register `/todo` path on Nancy routing table.
- The `View` attribute tells Nancy content negotiate which view template should pick up when requesting HTML.
- The `View` attribute path value (`Todo`) works with [Nancy view location convention](https://github.com/NancyFx/Nancy/wiki/View-location-conventions) to find out the proper view template to render. That is `/Views/Todo.html` in my example.

## Create Todo Item Page

Next, we are going to provide a page for creating a new Todo item. The new page is under route path `/todo/create`.

```html
<!-- todo create view, place as `/Views/TodoCreate.html` -->
<html>
    <body>
        <form>
            <input type="text" name="content" placeholder="Todo Content" />
            <input type="submit" />
        </form>
    </body>
</html>
```

```csharp
// todo create view model, place as `/ViewModels/TodoCreate.cs`
public class TodoCreate
{
    private readonly IDatabase database;

    [Get("/todo/create")]
    [View("TodoCreate")]
    public TodoCreate(IDatabase database)
    {
        this.database = database;
    }

    [Post("/todo/create")]
    public Response Post(IResponseFormatter response, Form form)
    {
        this.database.TodoItems.Add(form.Content);
        return response.AsRedirect("/todo");
    }

    public class Form
    {
        public string Content { get; set; }
    }
}
```

- There is a simple form in the view to post the data back to view model.
- In the view model, a method with `Post` attribute is provided to handle the posted request.
- After added the item to database, the method leverages the injected `response` to redirect to `/toto` page.

## Link Two Pages

The two pages are separated, it would be better to provide links in pages. So, user can negivate pages by links.

Add create todo items page to todo list page:

```html
<!-- add the following link to /Views/Todo.html -->
<a href="@Model.CreateTodoPage">Create todo items</a>
```

```csharp
// update Todo view model in `/ViewModels/Todo.cs`
public class Todo
{
    private readonly IUrlBuilder urlBuilder; // injected from constructor

    public string CreateTodoPage
    {
        get { return this.urlBuilder.GetUrl<TodoCreate>(() => new TodoCreate(null)); }
    }
}
```

Update the redirect url builder in `TodoCreate` view model:

```csharp
// update TodoCreate view model in `/ViewModels/TodoCreate.cs`
public class TodoCreate
{
    private readonly IUrlBuilder urlBuilder; // injected from constructor

    public Response Post(IResponseFormatter response, Form form)
    {
        // ... the code above
        string url = this.urlBuilder.GetUrl<Todo>(() => new Todo(null, null));
        return response.AsRedirect(url);
    }
}
```

- Note that we are using `IUrlBuilder` to construct URL on run time. `IUrlBuilder` in provided from Nancy.AttributeRouting too. There is no need to configure, it is registed on IoC container by default.
- Regarding to the APIs of `IUrlBuilder`, please check the **API** section below.

## Route and View Prefix

You might have notice, both page routing path has a same path `/todo`. It is a good practice to avoid redundant. Extract them as a route prefix.

```csharp
[RoutePrefix("todo")]
public abstract class TodoBase
{
}

public class Todo : TodoBase
{
    [Get("/")]
    public Todo(IDatabase database)
    {
        // ... the code above
    }
}

public class TodoCreate : TodoBase
{
    [Get("/create")]
    public TodoCreate(IDatabase database)
    {
        // .. the code above
    }

    [Post("/create")]
    public Response Post(IResponseFormatter response, Form form)
    {
        // ... the code above
    }
}
```

- Their both same route prefix is extracted to their base class attached `RoutePrefix` attribute.
- There is a similiar attrbiute with `View` attribute named `ViewPrefix` attribute.
- You can check the detail APIs of `RoutePrefix` and `ViewPrefix` attributes, please check the **API** section below.

## Conclusion

We created two simple pages with Nancy and AttributeRouting. Following MVVM pattern, only models (the IDatabase part), views and view models are created. In the view - view model part, we focus on how the view looks like, what functions we need to provide for the corresponding views.

Routing and view discovering are trivial jobs, use AttributeRouting for them.

# API

## RouteAttribute

## ViewAttribute

## RoutePrefixAttribute

## ViewPrefixAttribute

## IUrlBuilder

# License

MIT License.
