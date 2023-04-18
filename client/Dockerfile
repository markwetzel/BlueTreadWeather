FROM node:16 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:1.21
WORKDIR /app
COPY --from=build /app/build /usr/share/nginx/html
COPY --from=build /app/nginx.conf /app/nginx.conf
RUN envsubst '${PORT}' < /app/nginx.conf > /etc/nginx/conf.d/default.conf
CMD ["nginx", "-g", "daemon off;"]

