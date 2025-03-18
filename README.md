### Auth Service

## Overview

The Auth Service handles user authentication, authorization, and profile management for the Commune Drop platform. It serves as the central identity provider for all users, including customers and carriers.

## Features

- User registration and login
- JWT token generation and validation
- User profile management
- Role-based access control
- Password reset and account recovery
- Email verification


## Dependencies

- Email Service: For sending verification and password reset emails
- SMS Service: For two-factor authentication (optional)
- Notification Service: For account-related notifications


## Technology Stack

- Runtime: Node.js
- Framework: Express.js
- Database: MongoDB
- JWT: For token-based authentication
- Bcrypt: For password hashing
- Redis: For token blacklisting and rate limiting


## Setup

### Prerequisites

- Node.js v16+
- MongoDB
- Redis


### Quick Start

1. Clone the repository


```shellscript
git clone https://github.com/commune-drop/auth-service.git
cd auth-service
```

2. Install dependencies


```shellscript
npm install
```

3. Configure environment variables


```shellscript
cp .env.example .env
# Edit .env with your configuration
```

4. Start the service


```shellscript
npm start
```

For development:

```shellscript
npm run dev
```

## Configuration

Key environment variables:

```env
PORT=3000
MONGODB_URI=mongodb://localhost:27017/auth-service
JWT_SECRET=your_jwt_secret
JWT_EXPIRATION=1h
REFRESH_TOKEN_EXPIRATION=7d
EMAIL_SERVICE_URL=http://email-service:3007
NOTIFICATION_SERVICE_URL=http://notification-service:3005
```

## Security Features

- Password hashing with bcrypt
- JWT token expiration and refresh
- Rate limiting for login attempts
- Token blacklisting for logout
- HTTPS enforcement in production


## User Types

- Customer: Regular users who place delivery orders
- Carrier: Delivery personnel who fulfill orders
- Admin: Platform administrators with elevated privileges


## Events

The service publishes these events:

- `user.registered`
- `user.logged_in`
- `user.logged_out`
- `user.profile_updated`
- `user.password_changed`
- `user.account_verified`


## License

MIT
