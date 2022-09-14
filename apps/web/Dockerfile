FROM node:18-alpine
WORKDIR /app
COPY ./dist/apps/web .
ENV PORT=3333
EXPOSE ${PORT}
RUN npm install --production
CMD npm run start
