select 
u."Id",
u."LastName",
u."SurnameName", 
u."FirstName", 
u."Email",
u."Password",
r."Name" as "Role",
r."Id" as "RoleId"
from users as u
left join roles as r on u."RoleId"=r."Id"
