namespace QuestionLairClient.Services.Course;

public class CourseService
{
    private readonly HttpClient _http;

    public CourseService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        var response = await _http.GetAsync("api/Courses");

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Unable to access courses.");

        return await response.Content.ReadFromJsonAsync<List<Course>>() ?? new();
    }
}

public class Course
{
    public string Name { get; set; }
    public int Id { get; set; }
}
