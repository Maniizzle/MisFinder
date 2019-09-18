//using Microsoft.AspNetCore.Mvc;
//using MisFinder.Data.Persistence.IRepositories;
//using MisFinderData.Data.Contexts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MisFinder.Persistence.Repositories
//{
//    public class MisFinderRepository<TUser> : IMisFinderRepository<TUser> where TUser:class
//    {
//        private readonly MisFinderDbContext context;

//        public MisFinderRepository(MisFinderDbContext context)
//        {
//            this.context = context;
//        }

//        public IEnumerable<TUser> getAll()
//        {
//            List<TUser> cat = new List<TUser>();
//            cat = context.Set<TUser>().ToList();
//            return cat;
//          //  return await context.applicant.tolistasync();
//        }
        
//        public TUser Get(int id)
//        {
//            TUser cat = new TUser();
//            cat = context.Set<TUser>().Where(m => m.Id == id).FirstOrDefault();
//            return cat;
//            return applicant;
//        }
//        [HttpPost]
//        public void Post([FromBody] Applicant applicant)
//        {
//            context.Applicant.Add(applicant);
//            context.SaveChanges();
//        }


//        [httppost]
//        public void post([frombody] applicant candidate)
//        {
//            applicant cat = new applicant
//            {

//                firstname = candidate.firstname,
//                lastname = candidate.lastname,
//                course = candidate.course,
//                dateofbirth = candidate.dateofbirth,
//                emailaddress = candidate.emailaddress,
//                yeargraduated = candidate.yeargraduated
//                tagname = candidate.tagname,

//                userid = animal.userid,

//                animaltype = animal.animaltype


//            };
//            farm.animal.add(cat);
//            farm.savechanges();
//        }

//        [HttpPut("{id}")]
//        public void put(int id, RecordSheet record)
//        {
//            context.Entry(record).State = EntityState.Modified;
//            context.SaveChanges();
//        }

      
//        public void delete(int id)
//        {
//            animal user = farm.animal.find(id);
//            farm.animal.remove(user);
//            farm.savechanges();

//        }
//        // put: api/applicants/5
//        [httpput("{id}")]
//        public async task<iactionresult> putapplicant(int id, applicant applicant)
//        {
//            if (id != applicant.id)
//            {
//                return badrequest();
//            }

//            context.entry(applicant).state = entitystate.modified;

//            try
//            {
//                await context.savechangesasync();
//            }
//            catch (dbupdateconcurrencyexception)
//            {
//                if (!applicantexists(id))
//                {
//                    return notfound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return nocontent();
//        }

//        // post: api/applicants
//        [httppost]
//        public async task<actionresult<applicant>> postapplicant(applicant applicant)
//        {
//            context.applicant.add(applicant);
//            await context.savechangesasync();

//            return createdataction("getapplicant", new { id = applicant.id }, applicant);
//        }

        
//        public async Task Delete(int id)
//        {
//            var user = await context.Set<TUser>().FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            context.Set<TUser>().Remove(TUser);
//            await context.Savechangesasync();

//            return applicant;
//        }

//        private bool applicantexists(int id)
//        {
//            return context.applicant.any(e => e.id == id);
//        }

//    }
//}
