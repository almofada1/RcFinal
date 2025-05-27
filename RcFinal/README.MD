À primeira vez que o programa é executado numa nova base de dados, serão executadas 3 funções. 

A primeira será as migrações da ASP.NET, para criar as tabelas necessárias para a autenticação do sistema.
Pode se mudar qual será a base de dados escolhida na appsettings.json.

A segunda será o seed das roles nas tabelas da ASP.NET. Serão criadas 2 roles: Admin e User

A terceira será as migrações da aplicação em si, Quartos, Reservas, Estados(de quartos) e Packages(pacotes de hotel)

Também é criado uma conta administrador com conta especificada no appsettings.json