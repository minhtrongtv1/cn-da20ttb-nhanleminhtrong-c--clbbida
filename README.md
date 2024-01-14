# cn-da20ttb-nhanleminhtrong-c--clbbida
Xây dựng phần mềm quản lý câu lạc bộ Bida

Bước 1: Chúng ta có thể tải trực tiệp nội dung bài về hoặc dùng lệnh git như sau:

    gitclone https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida.git
    
Bước 2: Tiếp đó sau khi tải file hoặc gitclone về chúng ta tiến hành giải nén bài ra .

Bước 3: Chúng ta mở SQL Server lên ở đây mình dùng SSMS 19 (v19.0), sau khi vào SQL nhấp phải chuột vào "Databases" chọn "Restore Database" sau đó nhấn chọn "Device" .

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/5ff9f9a9-fbfc-46d4-bff9-2ff3fe8ef073)

Kế đó nháy nút ... cạnh bên Device để chọn file .bak để thêm CSDL vào .

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/724eb61d-01f3-4e2c-8817-f49369253192)

Bước 4: Chúng ta tiến hành mở Visual Studio ở đây mình dùng Visual Studio 2022 (NETFramework, Version= v4.8) và mở file .sln .

Bước 5: Sau khi mở thành công chúng ta vào ngay App.config để thay đổi tên Data Source và Catalog theo đúng tên CSDL .

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/e0e8f24f-1ca9-4efb-b5d9-2ae2934c6018)

Bước 6: Nhấn ![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/59a8a8a5-cf00-40cb-9e40-d3f34a58882d) để tiến hành chạy code .

Và đây là một vài giao diện của bài mình: 

**Giao diện đăng nhập**

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/63a1e4d1-2c77-4958-a80a-e8f33f985ce7)

**Giao diện trang chủ**

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/73fdb813-b841-40f1-aa80-40accd09cce9)

**Giao diện bàn Bida**

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/911e200e-df0a-4908-8f9a-ae62fe9c2b15)

**Giao diện thông tin bàn**

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/90b26042-2aae-4523-acd1-2c7153bb653f)

**Giao diện khách hàng vãng lai**

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/80d48705-ffb5-45d0-a817-6cfb344da8d2)

**Giao diện sản phẩm**

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/1388b6fe-c247-419f-9903-71edcf50be8f)

**Giao diện thống kê**

![image](https://github.com/minhtrongtv1/cn-da20ttb-nhanleminhtrong-c--clbbida/assets/131318631/9e4e5261-9b91-43dc-8c4e-0fba5df4d67a)
