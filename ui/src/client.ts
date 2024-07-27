import axios from 'axios';
import { UserDto, RegisterUserDto, AwardDto, AwardCreateDto, UserFinance } from './types';

const API_BASE_URL = 'https://localhost:7235/api';

export const fetchUsers = async (): Promise<UserDto[]> => {
  const response = await axios.get<UserDto[]>(`${API_BASE_URL}/users`);
  return response.data;
};

export const fetchAwardsByType = async (type: string): Promise<AwardDto[]> => {
  const response = await axios.get<AwardDto[]>(`${API_BASE_URL}/awards/${type}`);
  return response.data;
};

export const fetchAllAwards = async (): Promise<AwardDto[]> => {
    const response = await axios.get<AwardDto[]>(`${API_BASE_URL}/awards`);
    return response.data;
  };

  export const getTotalAwardAmountByDate = async (date: Date): Promise<number> => {
    const dateOnly = date.toISOString().split('T')[0];
    const response = await axios.get<number>(`${API_BASE_URL}/awards/totalAmountByDate`, {
      params: {
        date: dateOnly,
      },
    });
    return response.data;
  };
  

export const registerUser = async (user: RegisterUserDto): Promise<UserDto> => {
  const response = await axios.post<UserDto>(`${API_BASE_URL}/users`, user);
  return response.data;
};

export const createAward = async (award: AwardCreateDto): Promise<void> => {
  await axios.post(`${API_BASE_URL}/awards`, award);
};

export const fetchUserFinance = async (userId: number): Promise<UserFinance> => {
  const response = await axios.get<UserFinance>(`${API_BASE_URL}/userfinances/${userId}`);
  return response.data;
};

export const getAwardsByDate = async (date: Date): Promise<number> => {
  const response = await axios.get<number>(`${API_BASE_URL}/awards/search`, {
    params: {
      date: date.toISOString(),
    },
  });
  return response.data;
};

export const fetchUsersByRegistrationDate = async (date: Date): Promise<UserDto[]> => {
  const response = await axios.get<UserDto[]>(`${API_BASE_URL}/users/date`, {
    params: {
      date: date.toISOString(),
    },
  });
  return response.data;
};

export const fetchLatestWinnerForAward = async (awardId: number): Promise<UserDto> => {
const response = await axios.get<UserDto>(`${API_BASE_URL}/awards/lastWinner/${awardId}`);
return response.data;
};
