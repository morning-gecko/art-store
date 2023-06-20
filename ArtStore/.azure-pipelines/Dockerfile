# Stage 1: Build Angular Frontend
FROM node:latest AS build
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the Angular application
RUN npm run build --prod

# Stage 2: Serve Angular Frontend with NGINX
FROM nginx:latest

# Copy the built Angular files to NGINX
COPY --from=build /app/dist/ /usr/share/nginx/html

# Remove default NGINX configuration
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom NGINX configuration
COPY nginx.conf /etc/nginx/conf.d

# Expose port 80
EXPOSE 80

# Stage 3: Build and Run .NET Core Backend
FROM mcr.microsoft.com/dotnet/sdk:latest AS backend
WORKDIR /app

# Copy the .NET Core project file(s)
COPY YourCSharpApp.csproj .

# Restore dependencies
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the .NET Core application
RUN dotnet publish -c Release -o out

# Run the .NET Core application
CMD ["dotnet", "out/YourCSharpApp.dll"]