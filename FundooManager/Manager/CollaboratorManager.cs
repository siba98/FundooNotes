﻿using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository collaboratorRepository;
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            this.collaboratorRepository = collaboratorRepository;
        }

        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                return this.collaboratorRepository.AddCollaborator(collaborator);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteCollaborator(int NoteId)
        {
            try
            {
                return this.collaboratorRepository.DeleteCollaborator(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CollaboratorModel> GetCollaborator(int NoteId)
        {
            try
            {
                return this.collaboratorRepository.GetCollaborator(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
