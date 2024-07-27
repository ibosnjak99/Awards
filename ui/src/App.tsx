import React, { useEffect, useState } from 'react';
import Grid from '@mui/material/Grid';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import { fetchUsers, fetchAwardsByType, registerUser } from './client';
import { UserDto, RegisterUserDto, AwardDto } from './types';
import './App.css';
import RegisterForm from './RegisterForm';

function App() {
  const [users, setUsers] = useState<UserDto[]>([]);
  const [awards, setAwards] = useState<AwardDto[]>([]);
  const [open, setOpen] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const usersData = await fetchUsers();
        setUsers(usersData);
        
        const awardsData = await fetchAwardsByType('0');
        setAwards(awardsData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleRegister = async () => {
    const registrationDate = new Date().toISOString();
    const user: RegisterUserDto = { firstName, lastName, email, registrationDate };

    try {
      const registeredUser = await registerUser(user);
      console.log('User registered:', registeredUser);
      handleClose();
    } catch (error) {
      console.error('There was an error registering the user:', error);
    }
  };

  return (
    <div className="App">
      <h1>Awards</h1>
      <RegisterForm />
      <Grid container spacing={3} justifyContent="center">
        <Grid item xs={12} sm={6}>
          <Accordion>
            <AccordionSummary
              expandIcon={<ExpandMoreIcon />}
            >
              <Typography>All users</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <ul>
                {users?.map((user: UserDto) => (
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
              <Typography>All awards</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <ul>
                {awards?.map((award: AwardDto) => (
                  <li key={award.id}>
                    {award.name} - {award.amount}
                  </li>
                ))}
              </ul>
            </AccordionDetails>
          </Accordion>
        </Grid>
      </Grid>
    </div>
  );
}

export default App;
