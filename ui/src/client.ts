import axios from 'axios';
import { UserDto, RegisterUserDto, AwardDto, AwardCreateDto, UserFinanceDto, UserFinance } from './types'; // Adjust the import path as needed

const API_BASE_URL = 'https://localhost:7235/api';

export const fetchUsers = async (): Promise<UserDto[]> => {
  const response = await axios.get<UserDto[]>(`${API_BASE_URL}/users`);
  return response.data;
};

export const fetchAwardsByType = async (type: string): Promise<AwardDto[]> => {
  const response = await axios.get<AwardDto[]>(`${API_BASE_URL}/awards/${type}`);
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

export const updateUserBalance = async (userFinance: UserFinanceDto): Promise<void> => {
  await axios.post(`${API_BASE_URL}/userfinances/update-balance`, userFinance);
};
