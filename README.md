
# go2musicstore
E-Commerce Website covering the fundementals of AngularJS, WebAPI, ASP.NET MVC, Bootstrap, HTML/Javascript/CSS

With a keen interest in the open source community and the ever evolving web technologies, this is single page e-commerce web application, in its alpha stage. An alpha release of a fully functional website that allows end users to purchase albums of their favourite artists. Users can manage their own store account and an Administrator can manage the store inventory.

Based on a layered architecture, the Web App sits on top of a reusable API that exposes a StoreAccountManager and AlbumManager that provides Code-First Entity Framework CRUD operations to a back-end SQL Database. 

Ninject is used to resolve the API’s interfaces and perform Dependency Injection into my Web App’s MVC and WebAPI controllers. 

The Admin section html pages are passed to the browser from the server via ASP.NET MVC Razor Views. The main store’s html pages are served up to the client browser via client-side scripting using AngularJS MVC with ajax calls to the server’s RESTful WebAPI that performs CRUD operations to the SQL Database. 

SignalR is used to perform real-time updates of stock inventory upon purchases, to end users.

My intention is to also write a WPF front end album image uploader that also sits on-top of the re-usable API.

