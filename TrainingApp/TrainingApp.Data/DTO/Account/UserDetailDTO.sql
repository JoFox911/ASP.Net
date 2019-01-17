﻿select 
u.Id,
u.LastName,
u.SurName, 
u.FirstName, 
u.Email,
r.Name as Role,
r.Id as RoleId
from users as u
left join roles as r on u.RoleId=r.Id
where u.RecordState <> 4
