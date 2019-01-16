select 
u.Id,
CONCAT ( u.FirstName, ' ', u.LastName, ' ' , u.SurName )  as FullName, 
u.Email,
r.Name as Role
from users as u
left join roles as r on u.RoleId=r.Id