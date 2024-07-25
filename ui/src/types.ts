export interface RegisterUserDto {
    firstName: string;
    lastName: string;
    email: string;
    registrationDate: string;
  }
  
  export interface UserDto {
    pid: number;
    firstName: string;
    lastName: string;
    email: string;
    registrationDate: string;
  }
  
  export interface AwardDto {
    id: number;
    name: string;
    amount: number;
    periodType: number;
    startDate: string;
    isRecurring: boolean;
  }
  
  export interface AwardCreateDto {
    name: string;
    amount: number;
    periodType: number;
    startDate: string;
    isRecurring: boolean;
  }
  
  export interface UserFinanceDto {
    userId: number;
    amount: number;
  }
  
  export interface UserFinance {
    id: number;
    userId: number;
    balance: number;
  }
  