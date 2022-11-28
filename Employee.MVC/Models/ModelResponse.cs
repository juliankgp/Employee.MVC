namespace Employee.MVC.Models; public class ModelResponse<T> { public string Status { get; set; } public T Data { get; set; } public string Message { get; set; } }
