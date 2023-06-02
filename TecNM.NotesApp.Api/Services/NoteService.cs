using TecNM.NotesApp.Api.Repositories.Interfaces;
using TecNM.NotesApp.Api.Services.Interfaces;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Services;

public class NoteService: INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<NoteDto> SaveAsync(NoteDto noteDto)
    {
        var note = new Note()
        {
            Title = noteDto.Title,
            Description = noteDto.Description,
            idCategory = noteDto.idCategory,
            idUser = noteDto.idUser,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        
        note = await _noteRepository.SaveAsync(note);
        noteDto.Id = note.Id;

        return noteDto;
    }

    public async Task<NoteDto> UpdateAsync(NoteDto noteDto)
    {
        var note = await _noteRepository.GetById(noteDto.Id);

        if (note == null)
        {
            throw new Exception("Note Not Found");
        }

        note.Title = noteDto.Title;
        note.Description = noteDto.Description;
        noteDto.idUser = note.idUser;
        note.UpdatedBy = "";
        note.UpdatedDate = DateTime.Now;

        await _noteRepository.UpdateAsync(note);
        
        return noteDto;
    }

    public async Task<List<NoteDto>> GetAllByIdUserAsync(int idUser)
    {
        var notes = await _noteRepository.GetAllByIdUserAsync(idUser);
        var notesDto = notes.Select(c => new NoteDto(c)).ToList();
        return notesDto;
    }

    public async Task<bool> NoteExist(int id)
    {
        var note = await _noteRepository.GetById(id);

        return (note != null);
    }

    public async Task<NoteDto> GetById(int id)
    {
        var note = await _noteRepository.GetById(id);
        if (note == null)
        {
            throw new Exception("Note Not Found");
        }

        var noteDto = new NoteDto(note);

        return noteDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _noteRepository.DeleteAsync(id);
    }
}