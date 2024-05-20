using Testeger.Shared.Models;
using Blazored.LocalStorage;
using System.Text.Json;
using Testeger.Shared.Exceptions;

namespace Testeger.Shared.Services;

public class ProjectService
{
    private readonly ILocalStorageService _localStorage;
    private List<Project> _projects = new();
    private const string ProjectsKey = "projects";
    public event Action? OnChange;

    public ProjectService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        LoadProjects();
    }

    public IEnumerable<Project> GetProjects() => _projects;
    public int GetProjectCount() => _projects.Count;

 

    public void AddProject(Project project)
    {
        project.Id = Guid.NewGuid().ToString();
        _projects.Add(project);
        SaveProjects();
        NotifyStateChanged();
    }

    public void DeleteProject(string id)
    {
        var project = GetProjectById(id);

        _projects.Remove(project);
        SaveProjects();
        NotifyStateChanged();
    }

    public Project GetProjectById(string id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id) ?? throw new ProjectNotFoundException($"Project with id {id} was not found.");
        return project;
    }

    private async void LoadProjects()
    {
        var projectsJson = await _localStorage.GetItemAsync<string>(ProjectsKey);
        if (!string.IsNullOrEmpty(projectsJson))
        {
            _projects = JsonSerializer.Deserialize<List<Project>>(projectsJson) ?? new List<Project>();
        }
    }

    private async void SaveProjects()
    {
        var projectsJson = JsonSerializer.Serialize(_projects);
        await _localStorage.SetItemAsync(ProjectsKey, projectsJson);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
