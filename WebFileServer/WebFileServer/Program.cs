using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

string folderImage = "images";
//����� ���� �� �����
string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderImage);
//��������� ���� ����� � ������
Directory.CreateDirectory(folderPath);

//��������� ������ �� url - ������. �� ����� ����� �� ������
app.UseStaticFiles(new StaticFileOptions
{
    //����� �� ��� ����� ������� ������
    FileProvider = new PhysicalFileProvider(folderPath), //���� �� ������ ������� �� ����������� �����
    //��������� ������ ���� ��� ��� http://localhost:5298/images/1.jpg
    RequestPath = "/images" //������������ ��� url - �� ������
});


app.UseAuthorization();

app.MapControllers();

app.Run();
