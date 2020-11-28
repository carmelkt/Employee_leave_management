using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.DomainModels;

namespace LeaveSystem.Repositories
{
    public interface ILeavesRepository
    {
        void LeaveRequest(Leave q);
        void LeaveDetails(Leave q);
        void LeaveStatus(int qid, int status); //leaveid and status 
        void UpdateLeaveStatus(int qid, int status);
        List<Leave> GetLeaves();
        List<Leave> GetLeaveByLeaveID(int LeaveID);

    }
    public class LeavesRepository : ILeavesRepository
    {
        LeaveSystemDatabaseDbContext db;
        public LeavesRepository()
        {
            db = new LeaveSystemDatabaseDbContext();
        }
        public void LeaveRequest(Leave q)
        {
            db.Leaves.Add(q);
            db.SaveChanges();
        }
        public void LeaveDetails(Leave q)
        {
            Leave qt = db.Leaves.Where(temp => temp.LeaveID == q.LeaveID).FirstOrDefault();
            if (qt != null)
            {
                
                qt.LeaveStartDate = q.LeaveStartDate;
                qt.LeaveEndDate = q.LeaveEndDate;
                
                db.SaveChanges();
            }
        }
        public void LeaveStatus(int qid, int status)
        {
            Leave qt = db.Leaves.Where(temp => temp.LeaveID == qid).FirstOrDefault();
            if(qt!=null)
            {
                qt.LeaveStatus = status;
                db.SaveChanges();
            }
        }
        public void UpdateLeaveStatus(int qid, int status)
        {
            Leave qt = db.Leaves.Where(temp => temp.LeaveID == qid).FirstOrDefault();
            if(qt!=null)
            {
                qt.LeaveStatus = status;
                db.SaveChanges();
            }
        }
        public List<Leave> GetLeaves()
        {
            List<Leave> qt = db.Leaves.OrderBy(temp => temp.LeaveStartDate).ToList();
            return qt;
        }
        public List<Leave> GetLeaveByLeaveID(int LeaveID)
        {
            List<Leave> qt = db.Leaves.Where(temp => temp.LeaveID==LeaveID).ToList();
            return qt;
        }

    }
}
