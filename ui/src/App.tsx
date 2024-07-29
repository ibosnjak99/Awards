import React, { useEffect, useState } from 'react';
import Grid from '@mui/material/Grid';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Button from '@mui/material/Button';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import { fetchAwardsByType, fetchUsersByRegistrationDate, fetchLatestWinnerForAward, getTotalAwardAmountByDate } from './client';
import { UserDto, AwardDto } from './types';
import './App.css';
import RegisterForm from './components/RegisterForm';
import CreateAwardForm from './components/CreateAwardForm';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  const [awards, setAwards] = useState<AwardDto[]>([]);
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [registeredUsers, setRegisteredUsers] = useState<UserDto[]>([]);
  const [tabValue, setTabValue] = useState(0);
  const [awardWinners, setAwardWinners] = useState<{ [key: number]: UserDto }>({});
  const [totalAwardAmount, setTotalAwardAmount] = useState<number | null>(null);

  useEffect(() => {
    const fetchInitialAwards = async () => {
      try {
        const awardsData = await fetchAwardsByType('0');
        setAwards(awardsData);
        await fetchAwardWinners(awardsData);
        const today = new Date();
        const totalAmount = await getTotalAwardAmountByDate(today);
        setTotalAwardAmount(totalAmount);
        toast.success('Awards and total amount for today fetched successfully');
      } catch (error) {
        toast.error('Error fetching awards or total amount for today');
      }
    };

    fetchInitialAwards();
  }, []);

  const fetchAwardWinners = async (awards: AwardDto[]) => {
    try {
      const winners = await Promise.all(
        awards.map(async (award) => {
          const winner = await fetchLatestWinnerForAward(award.id);
          return { awardId: award.id, winner };
        })
      );
      const winnerMap = winners.reduce((acc, curr) => {
        acc[curr.awardId] = curr.winner;
        return acc;
      }, {} as { [key: number]: UserDto });
      setAwardWinners(winnerMap);
      toast.success('Award winners fetched successfully');
    } catch (error) {
      toast.error('Error fetching award winners');
    }
  };

  const handleFetchUsers = async () => {
    if (selectedDate) {
      try {
        const registeredUsersData = await fetchUsersByRegistrationDate(selectedDate);
        setRegisteredUsers(registeredUsersData);
        toast.success('Users fetched successfully');
      } catch (error) {
        toast.error('Error fetching users by registration date');
      }
    }
  };

  const handleTabChange = async (event: React.SyntheticEvent, newValue: number) => {
    setTabValue(newValue);
    try {
      const awardsData = await fetchAwardsByType(newValue.toString());
      setAwards(awardsData);
      await fetchAwardWinners(awardsData);
    } catch (error) {
      toast.error('Error fetching awards');
    }
  };

  return (
    <div className="App">
      <ToastContainer />
      <h1>Awards</h1>
      <RegisterForm />
      <CreateAwardForm />
      <div className="calendar-container">
        <Calendar
          onChange={setSelectedDate}
          value={selectedDate}
        />
        <Button
          sx={{ mt: 2 }}
          className="fetch-button"
          variant="contained"
          color="primary"
          onClick={handleFetchUsers}
        >
          Fetch users for selected date
        </Button>
      </div>
      <Grid container spacing={3} justifyContent="center">
        <Grid item xs={12} sm={6}>
          <Accordion>
            <AccordionSummary
              expandIcon={<ExpandMoreIcon />}
            >
              <Typography>Users registered on selected date</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <ul>
                {registeredUsers?.map((user: UserDto) => (
                  <li key={user.pid}>
                    {user.firstName} {user.lastName}
                  </li>
                ))}
              </ul>
            </AccordionDetails>
          </Accordion>
        </Grid>
        <Grid item xs={12} sm={6}>
          <Accordion>
            <AccordionSummary
              expandIcon={<ExpandMoreIcon />}
            >
              <Typography>All awards - Total amount for today: {totalAwardAmount}</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <Tabs value={tabValue} onChange={handleTabChange} aria-label="award tabs">
                <Tab label="Hourly" />
                <Tab label="Daily" />
                <Tab label="Weekly" />
                <Tab label="Monthly" />
              </Tabs>
              <div>
                <ul>
                  {awards?.map((award: AwardDto) => {
                    const winner = awardWinners[award.id];
                    return (
                      <li key={award.id}>
                        {award.name} | Award: {award.amount}KM | Last winner: {winner?.firstName} {winner?.lastName}
                      </li>
                    );
                  })}
                </ul>
              </div>
            </AccordionDetails>
          </Accordion>
        </Grid>
      </Grid>
    </div>
  );
}

export default App;
