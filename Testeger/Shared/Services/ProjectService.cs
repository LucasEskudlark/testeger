using Blazored.LocalStorage;
using System.Text.Json;
using Testeger.Shared.Exceptions;
using Testeger.Shared.Models.Entities;

namespace Testeger.Shared.Services;

public class ProjectService
{
    private readonly LocalStorageService _localStorage;
    private const string StorageKey = "projects";
    public event Action? OnChange;
    public event Action? OnProjectAdded;
    public event Action? OnProjectDeleted;
    public event Action? OnProjectUpdated;

    public ProjectService(LocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<List<Project>> GetAllProjects()
    {
        var projects = await _localStorage.ReadFromStorage<List<Project>>(StorageKey);
        return projects ?? new List<Project>();
    }
    
    public async Task<int> GetProjectCount()
    {
        var projects = await GetAllProjects();
        return projects.Count;
    }

    public async Task AddProject(Project project)
    {
        var projects = await GetAllProjects();

        if (projects.Contains(project))
        {
            projects.Remove(project);
        }
        projects.Add(project);

        await _localStorage.WriteToStorage(StorageKey, projects);
        OnProjectAdded?.Invoke();
        NotifyStateChanged();
    }

    public async Task RemoveProject(Project project)
    {
        var projects = await GetAllProjects();

        var projectToRemove = projects.Find(p => p.Id == project.Id);

        if (projectToRemove != null)
        {
            projects.Remove(projectToRemove);
        }

        await _localStorage.WriteToStorage(StorageKey, projects);
        OnProjectDeleted?.Invoke();
        NotifyStateChanged();
    }

    public async Task<Project> GetProjectById(string id)
    {
        var projects = await GetAllProjects();

        return projects.Find(p => p.Id == id) ?? throw new ProjectNotFoundException($"Project with id {id} was not found.");
    }

    public async Task UpdateProject(Project project)
    {
        var projects = await GetAllProjects();
        var index = projects.FindIndex(tr => tr.Id == project.Id);

        if (index == -1)
        {
            throw new ProjectNotFoundException($"Project with id {project.Id} was not found.");
        }

        projects[index] = project;
        await _localStorage.WriteToStorage(StorageKey, projects);
        OnProjectUpdated?.Invoke();
        NotifyStateChanged();
    }

    //public async Task RemoveProjectMember(string projectId, IList<User> users)
    //{
    //    var project = await GetProjectById(projectId);

    //    var userToBeRemoved = users.FirstOrDefault(); 

    //    if (userToBeRemoved is null)
    //    {
    //        return; // NEED FIX
    //    }

    //    var updatedMembers = project.Members.ToList();
    //    updatedMembers.Remove(userToBeRemoved);

    //    project.Members = updatedMembers.AsEnumerable();

    //    await UpdateProject(project);
    //}

    private void NotifyStateChanged() => OnChange?.Invoke();
}
