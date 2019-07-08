using System;
using System.Linq;

namespace library.Models
{
    public class RentDateValidator
    {
	    private readonly LibraryContext _context;

		public RentDateValidator(LibraryContext context)
		{
			_context = context;
		}
        
        public RentDateError Validate(DateTime start, DateTime end, Int32 volumeId)
        {
            if (start < DateTime.Today.Date)
                return RentDateError.StartInvalid;

            if (end < start)
                return RentDateError.EndInvalid;

            if (end == start)
                return RentDateError.LengthInvalid;
			
            Volume selectedVolume = _context.Volumes.FirstOrDefault(volume => volume.Id == volumeId);

            if (selectedVolume == null)
                return RentDateError.None;

            if (_context.Rents.Where(r => r.VolumeId == selectedVolume.Id && r.EndDate >= start)
                                .ToList()
                                .Any(r => r.IsConflicting(start, end)))
                return RentDateError.Conflicting;

            return RentDateError.None;
        }
    }
}