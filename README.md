BlogForDevelopersDotNet
=======================

Engine para criação de blog. Neste um projeto aplico os conceitos de Domain Driven Design, ASP.NET MVC 4, persistência de dados em XML e [Visual Studio Express 2012 for Web](http://www.microsoft.com/visualstudio/eng/downloads#d-2012-express).

Como utilizar
-------------
1 - Abra a solution ``BlogForDevelopers.sln``

2 - Rode o projeto ``BlogForDevelopers.Web``

O Blog
------
Layout inspirado no [Metro UI CSS](http://metroui.org.ua/), você pode remover e criar o layout que desejar. 

Area Administrativa
-------------------
Para acessar a área administrativa basta digitar ``/admin`` e usuário e senha será requisitado.

* ``usuário: admin``

* ``senha: admin``

Usuário e senha podem ser mudado no ``Web.config`` em:
```xml
<credentials passwordFormat="Clear">
  <user name="admin" password="admin" />
</credentials>
```

Nesta versão, é possível criar, editar e remover um post

Quem utiliza
-----------
* [jonatas saraiva](http://jonatassaraiva.net)

BackLog
----------------
* Envio de e-mail
* RSS Feed
* Conceito de pagina
* Conceito de conteúdo
* Comentários
* Busca
* Log de aplicação
* WebApi