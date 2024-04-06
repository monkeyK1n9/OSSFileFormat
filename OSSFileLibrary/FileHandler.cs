using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OSSFileLibrary.FileStructure;

namespace OSSFileLibrary
{
    public static class FileHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="numberOfJobs"></param>
        /// <param name="jobs"></param>
        /// <returns></returns>
        public static KingFile CreateFile(string name, UInt16 age, UInt16 numberOfJobs, Job[] jobs)
        {
            KingFile kingFile = new KingFile
            {
                MagicNumber = new char[] { 'k', 'i', 'n', 'g' },
                Version = 1,
                Name = name,
                Age = age,
                NumberOfJobs = numberOfJobs,
                Jobs = jobs
            };

            UInt32 offset = (uint)(kingFile.MagicNumber.Length + 2 + 4 + kingFile.Name.Length + 1 + 2 + 2 + (4 * kingFile.NumberOfJobs));

            UInt32[] offsets = new uint[kingFile.NumberOfJobs];
            for (int i = 0; i < offsets.Length; i++)
            {
                offsets[i] = offset;

                offset += (uint)(kingFile.Jobs[i].JobName.Length + 1 + 4);
            }
            kingFile.JobOffset = offsets;
            kingFile.FileSize = offset;

            return kingFile;
        }

        public static byte[] FileToByte(KingFile kingFile)
        {
            using MemoryStream memoryStream = new MemoryStream();
            using BinaryWriter writer = new BinaryWriter(memoryStream);

            writer.Write(kingFile.MagicNumber);
            writer.Write(kingFile.Version);
            writer.Write(kingFile.Name);
            writer.Write(kingFile.Age);
            writer.Write(kingFile.NumberOfJobs);

            foreach(UInt32 jobOffsets in kingFile.JobOffset)
            {
                writer.Write(jobOffsets);
            }

            foreach(Job job in kingFile.Jobs)
            {
                writer.Write(job.JobName);
                writer.Write(job.JobYear);
            }

            return memoryStream.ToArray();
        }

        public static KingFile ByteToFile(byte[] bytes)
        {
            using MemoryStream memoryStream = new MemoryStream(bytes);
            using BinaryReader reader = new BinaryReader(memoryStream);

            KingFile kingFile = new KingFile();

            kingFile.MagicNumber = reader.ReadChars(4);
            kingFile.Version = reader.ReadUInt16();
            kingFile.Name = reader.ReadString();
            kingFile.Age = reader.ReadUInt16();
            kingFile.NumberOfJobs = reader.ReadUInt16();

            UInt32[] offsets = new uint[kingFile.NumberOfJobs];
            for(int i = 0; i < offsets.Length; i++)
            {
                offsets[i] = reader.ReadUInt32();
            }

            kingFile.JobOffset = offsets;

            Job[] jobs = new Job[kingFile.NumberOfJobs];
            for(int i = 0; i < kingFile.NumberOfJobs; i++)
            {
                jobs[i].JobName = reader.ReadString();
                jobs[i].JobYear = reader.ReadUInt32();
            }
            kingFile.Jobs = jobs;

            return kingFile;
        }
    }
}
