using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task3.Core.DTO;
using Task3.Core.Models;

namespace Task3.Core.Services
{
    public interface IMaterialVersionServices
    {
        Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId);
        Task<IEnumerable<MaterialVersion>> FilterVersionBySize(Guid mId);
        Task<MaterialVersion> UploadNewMaterialVersion(UploadMaterialVersionDTO materialVersion);
        

        /*Task<IEnumerable<Music>> GetAllWithArtist();
        Task<Music> GetMusicById(int id);
        Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId);
        Task<Music> CreateMusic(Music newMusic);
        Task UpdateMusic(Music musicToBeUpdated, Music music);
        Task DeleteMusic(Music music);*/
    }
}