FROM node:16 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:1.21
WORKDIR /app
COPY --from=build /app/build /usr/share/nginx/html
COPY --from=build /app/default.conf /etc/nginx/conf.d/default.conf.template
CMD /bin/bash -c "envsubst '\$PORT' < /etc/nginx/conf.d/default.conf.template > /etc/nginx/nginx.conf && exec nginx -g 'daemon off;'"
