using Amirez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.SeatArrangement
{
    public class SeatArrangementRepository: GenericRepository<SeatArrangementEntity>, ISeatArrangementRepository
    {
        public SeatArrangementRepository(DatabaseContext context) : base(context)
        {

        }

        public List<string> GenerateFamilySeats(int familySize, List<string> availableSeats)
        {

            // Triez la liste en utilisant un comparateur personnalisé
            availableSeats.Sort(new CustomStringComparer());
            List<List<string>> neighborCombinations = GenerateNeighborCombinations(availableSeats, familySize);
            if (neighborCombinations.Count == 0)
            {
                // Not enough available seats
                return new List<string>();
            }
            else
            {
                var bestChoice = neighborCombinations.FirstOrDefault();
                if (bestChoice.Any())
                {
                    return bestChoice;
                }
            }
            return new List<string>();
        }

        public async Task<List<SeatArrangementEntity>> GetSeatArrangement(Guid flightId, IEnumerable<string> seats)
        {
            var res = await _context.SeatArrangements.Where(x => x.FlightId == flightId && seats.Any(y => y == x.SeatNumber)).ToListAsync();
            return res;
        }

        public async Task<IList<string>> GetAvailableSeats(Guid FlightId, int numberOfFamilyMembers)
        {
            var dispositionSieges = await _context.SeatArrangements
                .Where(ds => ds.FlightId == FlightId && ds.Status == true)
                .Select(sa => sa.SeatNumber)
                .ToListAsync();

            // Triez les sièges en utilisant une logique personnalisée pour la disposition 2-4-2
            dispositionSieges.Sort((s1, s2) =>
            {
                var rangee1 = GetRange(s1);
                var rangee2 = GetRange(s2);

                // Comparez les rangées en premier
                var rangéeComparison = rangee1.CompareTo(rangee2);
                if (rangéeComparison != 0)
                {
                    return rangéeComparison;
                }

                // Ensuite, comparez les sièges en fonction de leur position (gauche, milieu, droit)
                var position1 = GetPosition(s1);
                var position2 = GetPosition(s2);

                return position1.CompareTo(position2);
            });

            return GenerateFamilySeats(numberOfFamilyMembers, dispositionSieges);

        }
        public int GetRange(string seatNumber)
        {
            if (string.IsNullOrEmpty(seatNumber))
            {
                throw new ArgumentException("Le numéro de siège est vide ou nul.");
            }

            // Extract the row number, which is the numeric part of the seat number
            var rowNumber = string.Join("", seatNumber.ToCharArray().Where(Char.IsDigit));

            if (int.TryParse(rowNumber, out int range))
            {
                return range;
            }

            throw new ArgumentException("Numéro de rangée invalide dans le numéro de siège.");
        }
        public char GetPosition(string seatNumber)
        {
            if (string.IsNullOrEmpty(seatNumber))
            {
                throw new ArgumentException("Le numéro de siège est vide ou nul.");
            }

            // Extract the position, which is the last character of the seat number
            char position = seatNumber.First();

            if (position == 'A' || position == 'B' || position == 'C' || position == 'D')
            {
                return 'G'; // 'G' for Gauche (left)
            }
            else if (position == 'E' || position == 'F')
            {
                return 'M'; // 'M' for Milieu (middle)
            }
            else if (position == 'G' || position == 'H')
            {
                return 'D'; // 'D' for Droit (right)
            }
            else
            {
                throw new ArgumentException("Position invalide dans le numéro de siège.");
            }
        }

        private static List<List<string>> GenerateNeighborCombinations(List<string> sortedList, int n)
        {
            int totalColumns = sortedList.Count;
            if (n > totalColumns)
            {
                Console.WriteLine("Le nombre de colonnes à combiner est supérieur au nombre total de colonnes disponibles.");
                return new List<List<string>>();
            }

            List<List<string>> neighborCombinations = new List<List<string>>();

            for (int i = 0; i <= totalColumns - n; i++)
            {
                List<string> currentCombination = new List<string>();
                for (int j = i; j < i + n; j++)
                {
                    currentCombination.Add(sortedList[j]);
                }
                neighborCombinations.Add(currentCombination);
            }

            return neighborCombinations;
        }

        private class CustomStringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                char xLetter = x[0];
                char yLetter = y[0];
                int xNumber = int.Parse(x.Substring(1));
                int yNumber = int.Parse(y.Substring(1));

                // Triez d'abord par numéro
                int numberComparison = xNumber.CompareTo(yNumber);

                if (numberComparison == 0)
                {
                    // Si les numéros sont identiques, triez par lettre
                    return xLetter.CompareTo(yLetter);
                }
                else
                {
                    return numberComparison;
                }
            }
        }
    }
}
