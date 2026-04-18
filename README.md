# 📘 Facebook Page API - ASP.NET Core Web API

Dự án xây dựng REST API backend tích hợp với **Meta Graph API** để quản lý Facebook Page.

---

## 📁 Cấu trúc dự án

```
PAge_API/
├── Controllers/
│   └── PageController.cs       # Các endpoint chính
├── Models/
│   ├── PageInfoModel.cs        # Model token và page info
│   └── FacebookResponse.cs     # Model response từ Meta API
├── appsettings.json            # Cấu hình token và URL
├── Program.cs                  # Đăng ký service
└── README.md
```

---

## ⚙️ Cấu hình

Mở file `appsettings.json` và điền thông tin:

```json
{
  "Facebook": {
    "PageAccessToken": "YOUR_PAGE_ACCESS_TOKEN",
    "PageId": "YOUR_PAGE_ID",
    "BaseUrl": "https://graph.facebook.com/v25.0"
  }
}
```

### Cách lấy Page Access Token:

1. Truy cập [Graph API Explorer](https://developers.facebook.com/tools/explorer/)
2. Chọn ứng dụng **Page_API**
3. Chọn các quyền sau:
   - `pages_show_list`
   - `pages_read_engagement`
   - `pages_read_user_content`
   - `pages_manage_posts`
   - `read_insights`
4. Nhấn **Generate Access Token**
5. Gọi endpoint: `me/accounts`
6. Copy `access_token` và `id` của page vào `appsettings.json`

---

## 🚀 Chạy dự án

```bash
# Restore packages
dotnet restore

# Chạy dự án
dotnet run

# Hoặc nhấn F5 trong Visual Studio
```

Sau khi chạy, mở trình duyệt tại:
```
https://localhost:{port}/swagger
```

---

## 📡 Danh sách API Endpoints

### Base URL: `/api/page`

| Method | Endpoint | Mô tả |
|--------|----------|-------|
| GET | `/api/page/{pageId}` | Lấy thông tin Page |
| GET | `/api/page/{pageId}/posts` | Lấy danh sách bài viết |
| POST | `/api/page/{pageId}/posts` | Đăng bài mới lên Page |
| DELETE | `/api/page/post/{postId}` | Xóa bài viết |
| GET | `/api/page/{pageId}/insights` | Xem thống kê Page |
| GET | `/api/page/post/{postId}/comments` | Lấy bình luận của bài viết |
| GET | `/api/page/post/{postId}/likes` | Lấy lượt thích của bài viết |

---

## 📋 Chi tiết từng API

### 1. GET `/api/page/{pageId}`
Lấy thông tin cơ bản của Facebook Page.

**Response mẫu:**
```json
{
  "id": "1025870330617181",
  "name": "Nguyễn Văn Hưng",
  "category": "Công ty công nghệ thông tin",
  "fan_count": 0
}
```

---

### 2. GET `/api/page/{pageId}/posts`
Lấy danh sách bài viết của Page.

**Response mẫu:**
```json
{
  "data": [
    {
      "id": "1025870330617181_123456",
      "message": "Hello World!",
      "created_time": "2026-04-18T08:00:00+0000"
    }
  ]
}
```

---

### 3. POST `/api/page/{pageId}/posts`
Đăng bài viết mới lên Page.

**Request body:**
```json
{
  "message": "Nội dung bài viết"
}
```

**Response mẫu:**
```json
{
  "id": "1025870330617181_123456"
}
```

---

### 4. DELETE `/api/page/post/{postId}`
Xóa một bài viết theo ID.

**Response mẫu:**
```json
{
  "success": true
}
```

---

### 5. GET `/api/page/{pageId}/insights`
Lấy thống kê của Page (lượt tiếp cận, follower...).

**Response mẫu:**
```json
{
  "data": [
    {
      "name": "page_follows",
      "period": "day",
      "values": [
        { "value": 0, "end_time": "2026-04-18T07:00:00+0000" }
      ]
    }
  ]
}
```

> ⚠️ **Lưu ý:** Page mới tạo hoặc chưa có hoạt động sẽ trả về data rỗng. Đây là giới hạn từ phía Meta, không phải lỗi code.

---

### 6. GET `/api/page/post/{postId}/comments`
Lấy danh sách bình luận của một bài viết.

**Response mẫu:**
```json
{
  "data": [
    {
      "id": "comment_123",
      "message": "Bình luận hay quá!",
      "created_time": "2026-04-18T09:00:00+0000"
    }
  ]
}
```

---

### 7. GET `/api/page/post/{postId}/likes`
Lấy danh sách người đã thích bài viết.

**Response mẫu:**
```json
{
  "data": [
    {
      "id": "user_123",
      "name": "Nguyễn Văn A"
    }
  ]
}
```

---

## 🔐 Quyền (Permissions) cần thiết

| Quyền | Dùng cho |
|-------|---------|
| `pages_show_list` | Lấy danh sách page |
| `pages_read_engagement` | Đọc comments, likes, insights |
| `pages_read_user_content` | Đọc bài viết của page |
| `pages_manage_posts` | Tạo và xóa bài viết |
| `read_insights` | Xem thống kê page |

---

## 🛠️ Công nghệ sử dụng

| Công nghệ | Phiên bản |
|-----------|-----------|
| ASP.NET Core Web API | .NET 8 |
| Meta Graph API | v25.0 |
| Swagger / OpenAPI | Swashbuckle |
| HttpClient | Built-in |

---

## ⚠️ Lưu ý quan trọng

- **Page Access Token** có thời hạn, cần lấy lại khi hết hạn
- **Insights API** chỉ trả data sau khi page có hoạt động thực tế
- Không commit `PageAccessToken` lên Git — nên dùng biến môi trường khi deploy
- App Meta phải ở chế độ **Live** để dùng với tài khoản thật (ngoài tester)

---

## 👨‍💻 Tác giả

**Nguyễn Văn Hưng**  
Bài tập: Facebook API - Page API  
Ngày: 18/04/2026
