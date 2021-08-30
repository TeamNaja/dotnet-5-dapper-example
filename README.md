# dotnet-5-dapper-example
ตัวอย่าง .net 5 api + dapper อย่างง่าย ใน 5 นาที

## สำหรับไว้ศึกษาและเรียนรู้ 

Dapper คือ Micro ORM สำหรับใช้งานคุ่กับคำสั่ง SQL 
โดยข้อดีของ Dapper คือ ความเร็วเพราะว่าเราเขียนคำสั่งโดยตรง (ถ้าใช้ ORM เจ้าอื่นเช่น EF Core จะต้องแปลง Linq เป็น คำสั่ง SQL อีกที)
เหมาะสำหรับ Services ที่ต้องต่อ Database ประเภท Relational-Database เช่น SQL Server, Postgres, Oracle 

ขั้นตอน 
1. ติดตั้ง Docker 
https://github.com/code-naja/docker-installation-example

2. ติดตั้ง Visual Studio 2019 Community 

3. Clone Code ลงไปที่เครื่อง 

4. ไปที่ Folder Code ที่ Clone ไป จากนั้นเปิด Terminal ขึ้น 
![image](https://user-images.githubusercontent.com/71694878/131341693-0a7bd2be-f849-40e2-aef4-5706b02e937c.png)
![image](https://user-images.githubusercontent.com/71694878/131341735-4cd8d4d9-4167-48c5-87cd-d0718de9bf0e.png)


5. docker-compose up -d 
docker-compose คือการเรียกไฟล์ docker-compose.yml ไฟล์ 
ซึ่งในนั้นจะมีชุดคำสั่งสำหรับ run docker อยู่ 
up คือ สั่งให้มันทำงาน
-d คือ detach คือ ทำงานแบบ background ไม่ต้องให้ terminal เราไปผูกกับ docker นี้ 
![image](https://user-images.githubusercontent.com/71694878/131341894-929cb2a9-bcb1-4e7a-988e-d9af6169f9e9.png)


6. เปิด Project 
7. กด Run 
8. ดูคำอธิบายต่าง ๆ ใน code ว่าแต่ละอย่างคืออะไร
9. ถ้าสงสัยหม่องใด๋ ให้ไปสร้าง issue แคปรูปมาถาม 
10. เล่นเบิ่ง

