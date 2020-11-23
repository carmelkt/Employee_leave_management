using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.DomainModels;
using LeaveSystem.ViewModels;
using LeaveSystem.Repositories;
using AutoMapper;
using AutoMapper.Configuration;


namespace LeaveSystem.ServiceLayer
{
    public interface ILeavesService
    {
        void LeaveRequest(LeaveViewModel qvm);
        void LeaveDetails(UpdateLeaveViewModel q);
        void LeaveStatus(int qid, int status); //leaveid and status 
        void UpdateLeaveStatus(int qid, int status);
        List<LeaveViewModel> GetLeaves();
        LeaveViewModel GetLeaveByLeaveID(int LeaveID);

    }
    public class LeavesService: ILeavesService
    {
        ILeavesRepository qr;
        public LeavesService()
        {
            qr = new LeavesRepository();
        }
        public void LeaveRequest(LeaveViewModel qvm) //request leave was changed to leave view model
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave q = mapper.Map<LeaveViewModel, Leave>(qvm);
            qr.LeaveRequest(q);
        }
        public void LeaveDetails(UpdateLeaveViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateLeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave q = mapper.Map<UpdateLeaveViewModel, Leave>(qvm);
            qr.LeaveDetails(q);
        }
        public void LeaveStatus(int qid, int status)
        {
            qr.LeaveStatus(qid, status);
        }

        public void UpdateLeaveStatus(int qid, int status)
        {
            qr.UpdateLeaveStatus(qid, status);
        }

        public List<LeaveViewModel> GetLeaves()
        {
            List<Leave> q = qr.GetLeaves();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> qvm = mapper.Map<List<Leave>, List<LeaveViewModel>>(q);
            return qvm;
        }
        public LeaveViewModel GetLeaveByLeaveID(int LeaveID)
        {
            Leave q = qr.GetLeaveByLeaveID(LeaveID).FirstOrDefault();
            LeaveViewModel qvm = null;
            if (q != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Leave, LeaveViewModel>(q);
            }
            return qvm;
        }
    }
}
