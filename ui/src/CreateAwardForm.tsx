import React, { useState } from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Box from '@mui/material/Box';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import FormControl from '@mui/material/FormControl';
import FormLabel from '@mui/material/FormLabel';
import { createAward } from './client';
import { AwardCreateDto } from './types';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

enum PeriodType {
  Hourly = 0,
  Daily = 1,
  Weekly = 2,
  Monthly = 3,
}

const CreateAwardForm = () => {
  const [open, setOpen] = useState(false);
  const [name, setName] = useState('');
  const [amount, setAmount] = useState<number | ''>('');
  const [periodType, setPeriodType] = useState<PeriodType>(PeriodType.Hourly);
  const [isRecurring, setIsRecurring] = useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleCreateAward = async () => {
    if (name && amount !== '') {
      const award: AwardCreateDto = {
        name,
        amount: Number(amount),
        periodType,
        isRecurring,
      };
      try {
        await createAward(award);
        toast.success('Award created successfully');
        setName('');
        setAmount('');
        setPeriodType(PeriodType.Hourly);
        setIsRecurring(false);
        handleClose();
      } catch (error) {
        toast.error('Error creating award');
      }
    }
  };

  return (
    <div className="create-award-form">
      <Button variant="outlined" color="primary" onClick={handleClickOpen}>
        New Award
      </Button>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>New Award</DialogTitle>
        <DialogContent>
          <Box component="form" noValidate autoComplete="off">
            <TextField
              label="Award Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Amount"
              type="number"
              value={amount}
              onChange={(e) => setAmount(Number(e.target.value))}
              fullWidth
              margin="normal"
            />
            <FormControl component="fieldset">
              <FormLabel component="legend">Period Type</FormLabel>
              <RadioGroup
                value={periodType}
                onChange={(e) => setPeriodType(Number(e.target.value) as PeriodType)}
              >
                <FormControlLabel value={PeriodType.Hourly} control={<Radio />} label="Hourly" />
                <FormControlLabel value={PeriodType.Daily} control={<Radio />} label="Daily" />
                <FormControlLabel value={PeriodType.Weekly} control={<Radio />} label="Weekly" />
                <FormControlLabel value={PeriodType.Monthly} control={<Radio />} label="Monthly" />
              </RadioGroup>
            </FormControl>
            <FormControlLabel
              control={
                <Checkbox
                  checked={isRecurring}
                  onChange={(e) => setIsRecurring(e.target.checked)}
                />
              }
              label="Is Recurring"
            />
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleCreateAward} color="primary">
            Create Award
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default CreateAwardForm;
