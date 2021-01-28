using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task3.Core.DTO;
using Task3.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Task3.Core.Services
{
    public interface IMaterialVersionServices
    {
        Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId);
        Task<IEnumerable<MaterialVersion>> FilterVersionsBySize(Guid mId);
        
        Task<MaterialVersion> UploadNewMaterialVersion(UploadMaterialVersionDTO materialVersion);
        Task<MaterialVersion> DownloadMaterialVersion(DownloadMaterialVersionDTO materialId);


        /*Task<IEnumerable<Music>> GetAllWithArtist();
        Task<Music> GetMusicById(int id);
        Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId);
        Task<Music> CreateMusic(Music newMusic);
        Task UpdateMusic(Music musicToBeUpdated, Music music);
        Task DeleteMusic(Music music);*/
    }
}