var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Cấu hình dịch vụ lưu giữ trạng thái phiên làm việc (Session)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Kích hoạt sử dụng Session
app.UseAuthorization();

app.MapRazorPages();
app.Run();