# Build stage
FROM node:16 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
ARG REACT_APP_API_URL
RUN [ -z "$REACT_APP_API_URL" ] && cp .env .env.production || echo "REACT_APP_API_URL=$REACT_APP_API_URL" > .env.production
RUN npm run build

# Runtime stage
FROM nginx:1.21
WORKDIR /app
COPY --from=build /app/build /usr/share/nginx/html
COPY --from=build /app/default.conf /etc/nginx/conf.d/default.conf.template
CMD /bin/bash -c "envsubst '\$PORT' < /etc/nginx/conf.d/default.conf.template > /etc/nginx/conf.d/default.conf && exec nginx -g 'daemon off;'"
